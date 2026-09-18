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
        Vector3 screenPosition = cursorPosition;

        screenPosition.z = 0;

        transform.position = screenPosition;

        if (selectedTower != null )
        {
            selectedTower.transform.position = screenPosition;
        }
    }

    private void SetCursorScreenPosition(Vector2 mousePosition)
    {
        cursorPosition = Camera.main.ScreenToWorldPoint(mousePosition);
    }
    private void Action()
    {
        Debug.Log("Action was called");

        Vector3 screenPosition = cursorPosition;

        if (selectedTower != null)
        {
            selectedTower.Unselected();
            selectedTower = null;
        }
        else
        {
            RaycastHit2D hit = Physics2D.Raycast(screenPosition, Vector2.zero);

            if (hit.collider.TryGetComponent<Tower>(out Tower tower))
            {
                if (selectedTower == tower)
                {
                    selectedTower.Unselected();
                    selectedTower = null;
                }
                else
                {
                    selectedTower = tower;
                    selectedTower.Selected();
                }

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
