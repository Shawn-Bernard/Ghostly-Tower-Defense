using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System;

[CreateAssetMenu(fileName = "Player Actions", menuName = "Input Action Reader/Player Actions")]
public class PlayerInputActions : ScriptableObject, InputActions.IPlayerActions
{
    private InputActions inputActions;

    #region Move Events
    public event UnityAction<Vector2> MoveEvent;
    public event UnityAction MoveStartedEvent;
    public event UnityAction MoveCanceledEvent;
    #endregion

    #region Action Events
    public event UnityAction ActionStartedEvent;
    public event UnityAction ActionCanceledEvent;
    #endregion

    #region Interact Events
    public event UnityAction InteractStartedEvent;
    public event UnityAction InteractCanceledEvent;
    public event UnityAction InteractPerformedEvent;

    #endregion


    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());

        if (context.started)
        {
            MoveStartedEvent?.Invoke();
        }

        if (context.canceled)
        {
            MoveCanceledEvent?.Invoke();
        }
    }

    public void OnAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ActionStartedEvent?.Invoke();
        }

        if (context.canceled)
        {
            ActionCanceledEvent?.Invoke();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            InteractStartedEvent?.Invoke();
        }

        if (context.canceled)
        {
            InteractCanceledEvent?.Invoke();
        }

        if (context.performed)
        {
            InteractPerformedEvent?.Invoke();
        }
    }

    private void OnEnable()
    {
        inputActions = new InputActions();
        inputActions.Player.SetCallbacks(this);
        inputActions.Player.Enable();
    }
    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    
}
