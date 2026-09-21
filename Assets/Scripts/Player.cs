using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInputActions inputActions;
    [SerializeField] private float sensitivity;

    private Tower selectedTower;

    private Vector2 cursorPosition;

    [SerializeField] float towerPickupDistance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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

        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit2D hit = hits[i];

            if (hit.collider == null) return;
            Debug.Log(hit.collider.name);
            if (hit.collider.TryGetComponent<Tower>(out Tower tower) && selectedTower == null)
            {
                selectedTower = tower;
                selectedTower.Selected();
                Debug.Log("breaking out");
                break;
            }

            if (hit.collider.CompareTag("Placeable") && selectedTower != null)
            {
                Debug.Log("has selected tower now about to unselect ");
                selectedTower.Unselected();
                selectedTower = null;
            }
        }
    }

    //private void Handle()

    private void SetSelectedTower(Tower tower)
    {
        selectedTower = tower;
    }

    private void OnEnable()
    {
        inputActions.MoveEvent += SetCursorScreenPosition;
        inputActions.ActionStartedEvent += Action;
    }

    private void OnDisable()
    {
        inputActions.MoveEvent -= SetCursorScreenPosition;
        inputActions.ActionStartedEvent -= Action;
    }

}
