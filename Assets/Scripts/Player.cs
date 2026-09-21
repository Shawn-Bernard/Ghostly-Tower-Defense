using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInputActions inputActions;
    [SerializeField] private Inventory inventory;

    private Tower selectedTower;

    private Vector2 cursorPosition;

    [SerializeField] float towerPickupDistance;

    [SerializeField] TowerEvent selectedTowerEvent;
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

        if (selectedTower != null )
        {
            selectedTower.transform.position = onScreenPosition;
        }
    }

    private void SetCursorScreenPosition(Vector2 mousePosition)
    {
        cursorPosition = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void Action()
    {

        Vector3 onScreenPosition = cursorPosition;

        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.transform.position, onScreenPosition);

        foreach (var hit in hits)
        {
            if (hit.collider == null) continue;

            if (hit.collider.TryGetComponent<Tower>(out Tower tower) &&
                selectedTower == null)
            {
                if (Vector2.Distance(onScreenPosition, tower.transform.position) <= towerPickupDistance)
                {
                    selectedTower = tower;
                    selectedTower.Selected();

                    break;
                }
            }

            if (hit.collider.CompareTag("Placeable") && selectedTower != null)
            {
                // Comparing distance to see if u can place tower
                if (!inventory.IsTowerTooClose(selectedTower))
                {
                    Debug.Log("has selected tower now about to unselect ");
                    selectedTower.Unselected();
                    selectedTower = null;
                    break;
                }
            }
        }
    }

    //private void Handle()

    private void SetSelectedTower(Tower tower)
    {
        if (selectedTower != null)
        {
            selectedTower.Cancelled();
        }
        selectedTower = tower;
    }

    private void OnEnable()
    {
        inputActions.MoveEvent += SetCursorScreenPosition;
        inputActions.ActionStartedEvent += Action;

        selectedTowerEvent.registerEvent += SetSelectedTower;
    }

    private void OnDisable()
    {
        inputActions.MoveEvent -= SetCursorScreenPosition;
        inputActions.ActionStartedEvent -= Action;

        selectedTowerEvent.registerEvent -= SetSelectedTower;
    }

}
