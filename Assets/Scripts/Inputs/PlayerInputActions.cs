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

    #region Unselect Events
    public event UnityAction UnselectPerformedEvent;
    #endregion

    #region Pause Events
    public event UnityAction PauseStartedEvent;
    public event UnityAction PauseCanceledEvent;
    #endregion


    #region Slot 1 Events
    public event UnityAction Slot1StartedEvent;
    public event UnityAction Slot1PerformedEvent;
    #endregion

    #region Slot 2 Events
    public event UnityAction Slot2StartedEvent;
    public event UnityAction Slot2PerformedEvent;
    #endregion

    #region Slot 3 Events
    public event UnityAction Slot3StartedEvent;
    public event UnityAction Slot3PerformedEvent;
    #endregion

    #region Slot 4 Events
    public event UnityAction Slot4StartedEvent;
    public event UnityAction Slot4PerformedEvent;
    #endregion

    #region Slot 5 Events
    public event UnityAction Slot5StartedEvent;
    public event UnityAction Slot5PerformedEvent;
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
    public void OnUnselect(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            UnselectPerformedEvent?.Invoke();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            PauseStartedEvent?.Invoke();
        }

        if (context.canceled)
        {
            PauseCanceledEvent?.Invoke();
        }
    }
    public void OnTowerSlot1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Slot1StartedEvent?.Invoke();
        }

        if (context.performed)
        {
            Slot1PerformedEvent?.Invoke();
        }
    }

    public void OnTowerSlot2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Slot2StartedEvent?.Invoke();
        }

        if (context.performed)
        {
            Slot2PerformedEvent?.Invoke();
        }
    }

    public void OnTowerSlot3(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Slot3StartedEvent?.Invoke();
        }

        if (context.performed)
        {
            Slot3PerformedEvent?.Invoke();
        }
    }
    public void OnTowerSlot4(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Slot4StartedEvent?.Invoke();
        }

        if (context.performed)
        {
            Slot4PerformedEvent?.Invoke();
        }
    }

    public void OnTowerSlot5(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Slot5StartedEvent?.Invoke();
        }

        if (context.performed)
        {
            Slot5PerformedEvent?.Invoke();
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
