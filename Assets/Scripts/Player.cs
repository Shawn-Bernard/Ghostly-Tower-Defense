using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInputActions inputActions;
    [SerializeField] private Inventory inventory;

    private Weapon selectedWeapon;

    private Vector2 cursorPosition;

    [SerializeField] float towerPickupDistance;

    [SerializeField] WeaponEvent selectedWeaponEvent;

    [SerializeField] private GameState gameplayState;
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
        if (selectedWeapon != null)
        {
            selectedWeapon.transform.position = cursorPosition;
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

        foreach (var hit in hits)
        {
            if (hit == null) continue;
            if (hit.TryGetComponent<Weapon>(out Weapon weapon) &&
                selectedWeapon == null)
            {
                if (Vector2.Distance(onScreenPosition, weapon.transform.position) <= towerPickupDistance)
                {
                    selectedWeapon = weapon;
                    selectedWeapon.Selected();

                    break;
                }
            }

            if (selectedWeapon != null)
            {
                if (selectedWeapon.TryGetComponent(out Tower tower)) // Only towers care about placecment
                {
                    // Comparing distance to see if u can place tower
                    if (hit.CompareTag("Placeable") &&
                        !inventory.IsTowerTooClose(tower))
                    {
                        selectedWeapon.Unselected();
                        selectedWeapon = null;
                        break;
                    }
                }
                else
                {
                    selectedWeapon.Unselected();
                    selectedWeapon = null;
                    break;
                }
            }
        }
    }

    private void DisableInputs()
    {
        inputActions.MoveEvent -= SetCursorScreenPosition;
        inputActions.ActionStartedEvent -= Action;
    }

    private void EnableInputs()
    {
        inputActions.MoveEvent += SetCursorScreenPosition;
        inputActions.ActionStartedEvent += Action;
    }

    private void SetSelectedTower(Weapon newSelectedWeapon)
    {
        if (selectedWeapon != null)
        {
            selectedWeapon.Cancelled();
        }
        selectedWeapon = newSelectedWeapon;
        selectedWeapon.Selected();
    }

    private void OnEnable()
    {
        gameplayState.onEnterState += EnableInputs;
        gameplayState.onExitState += DisableInputs;

        selectedWeaponEvent.gameEvent += SetSelectedTower;
    }

    private void OnDisable()
    {
        gameplayState.onEnterState += EnableInputs;
        gameplayState.onExitState += DisableInputs;

        selectedWeaponEvent.gameEvent -= SetSelectedTower;
    }
}
