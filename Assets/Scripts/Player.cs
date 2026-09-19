using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInputActions inputActions;
    [SerializeField] private float sensitivity;

    private Tower selectedTower;

    private Vector2 cursorPosition;
    
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


        RaycastHit2D hit = Physics2D.Raycast(onScreenPosition, Vector2.zero);

        if (hit.collider == null) return;
        if (hit.collider.TryGetComponent<Tower>(out Tower tower))
        {
            if (selectedTower == null)
            {
                selectedTower = tower;
                selectedTower.Selected();
            }
        }

        if (hit.collider.CompareTag("Placeable"))
        {
            if (selectedTower != null)
            {
                selectedTower.Unselected();
                selectedTower = null;
            }
        }
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
