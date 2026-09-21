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
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory ??= GetComponent<Inventory>();
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 onScreenPosition = cursorPosition;

        onScreenPosition.z = 0;

        transform.position = onScreenPosition;

        if (selectedWeapon != null )
        {
            selectedWeapon.transform.position = onScreenPosition;
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

    //private void Handle()

    private void SetSelectedTower(Weapon newSelectedWeapon)
    {
        if (selectedWeapon != null)
        {
            selectedWeapon.Cancelled();
        }
        selectedWeapon = newSelectedWeapon;
    }

    private void OnEnable()
    {
        inputActions.MoveEvent += SetCursorScreenPosition;
        inputActions.ActionStartedEvent += Action;

        selectedWeaponEvent.gameEvent += SetSelectedTower;
    }

    private void OnDisable()
    {
        inputActions.MoveEvent -= SetCursorScreenPosition;
        inputActions.ActionStartedEvent -= Action;

        selectedWeaponEvent.gameEvent -= SetSelectedTower;
    }
}
