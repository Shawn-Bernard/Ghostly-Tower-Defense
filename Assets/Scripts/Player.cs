using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInputActions inputActions;
    [SerializeField] private Inventory inventory;

    private Weapon selectedWeapon;
    private Weapon weaponToPlace;

    private Vector2 cursorPosition;

    [SerializeField] float towerPickupDistance;

    [SerializeField] WeaponEvent selectedWeaponEvent;

    [SerializeField] private GameState gameplayState;

    [SerializeField] private UnityEvent onSelectedWeapon;
    [SerializeField] private UnityEvent onUnselectedWeapon;
    [SerializeField] private UnityEvent pauseEvent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory ??= GetComponent<Inventory>();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = cursorPosition;
        HoldSelectedWeapon();
    }

    private void HoldSelectedWeapon()
    {
        if (weaponToPlace != null)
        {
            weaponToPlace.transform.position = cursorPosition;
        }
    }

    private void SetCursorScreenPosition(Vector2 mousePosition)
    {
        cursorPosition = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void Action()
    {

        Vector3 onScreenPosition = cursorPosition;

        Collider2D[] hits = Physics2D.OverlapPointAll(onScreenPosition);

        

        // Deals with inventory weapon to be placed
        if (weaponToPlace != null) 
        {
            // Only towers care about placecment
            if (weaponToPlace.TryGetComponent(out Tower tower))
            {
                foreach (var hit in hits)
                {
                    if (hit == null) continue;

                    if (!hit.CompareTag("Placeable")) continue;
                    // Comparing distance to see if u can place tower
                    if (!inventory.IsTowerTooClose(tower))
                    {
                        weaponToPlace.Unselected();
                        weaponToPlace = null;
                        break;
                    }
                }
            }
            else // If its not a tower then deselect
            {
                weaponToPlace.Unselected();
                weaponToPlace = null;
            }
        }

        
        // Deals with selecting a weapon
        foreach (var hit in hits)
        {
            if (hit == null) continue;

            if (hit.TryGetComponent<Weapon>(out Weapon weapon))
            {
                if (Vector2.Distance(onScreenPosition, weapon.transform.position) <= towerPickupDistance)
                {
                    if (selectedWeapon != null)
                    {
                        selectedWeapon.Unselected();
                    }
                    selectedWeapon = weapon;
                    selectedWeapon.Selected();
                    onSelectedWeapon?.Invoke();
                    break;
                }
            }
        }
    }


    private void SetSelectedTower(Weapon newSelectedWeapon)
    {
        if (weaponToPlace != null)
        {
            Destroy(weaponToPlace.gameObject);
        }

        weaponToPlace = newSelectedWeapon;

        if (weaponToPlace != null)
        {
            weaponToPlace.Selected();
        }
    }

    private void UnselectWeapon()
    {
        if (selectedWeapon != null)
        {
            selectedWeapon.Unselected();
            selectedWeapon = null;
            onUnselectedWeapon?.Invoke();
        }
    }
    private void Pause()
    {
        pauseEvent?.Invoke();
    }

    public void DestroySelectedWeapon()
    {
        if (selectedWeapon != null)
        {
            Destroy(selectedWeapon.gameObject);

            selectedWeapon = null;
            onUnselectedWeapon?.Invoke();
        }
    }

    private void DisableInputs()
    {
        inputActions.MoveEvent -= SetCursorScreenPosition;
        inputActions.ActionStartedEvent -= Action;
        inputActions.UnselectPerformedEvent -= UnselectWeapon;

    }

    private void EnableInputs()
    {
        inputActions.MoveEvent += SetCursorScreenPosition;
        inputActions.ActionStartedEvent += Action;
        inputActions.UnselectPerformedEvent += UnselectWeapon;
    }

    private void OnEnable()
    {
        gameplayState.onEnterState += EnableInputs;
        gameplayState.onExitState += DisableInputs;

        inputActions.PauseStartedEvent += Pause;

        selectedWeaponEvent.gameEvent += SetSelectedTower;
    }

    private void OnDisable()
    {
        gameplayState.onEnterState -= EnableInputs;
        gameplayState.onExitState -= DisableInputs;

        inputActions.PauseStartedEvent -= Pause;

        selectedWeaponEvent.gameEvent -= SetSelectedTower;
    }
}
