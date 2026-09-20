using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Game State",menuName = "Game State")]
public class GameState : ScriptableObject
{
    public Action onEnterState;

    public Action onUpdateState;

    public Action onExitState;
    public void EnterState() => onEnterState?.Invoke();

    public void UpdateState() => onUpdateState?.Invoke();

    public void ExitState() => onExitState?.Invoke();
}