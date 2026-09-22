using UnityEngine;

public class GameStateManager : MonoBehaviour
{

    [SerializeField] private GameState mainMenu;
    [SerializeField] private GameState gameplayState;
    [SerializeField] private GameState pauseState;
    [SerializeField] private GameState gameOverState;

    private GameState currentState;
    private GameState lastState;

    [SerializeField] private VoidEvent onMainMenu;
    [SerializeField] private VoidEvent onGameplay;

    [SerializeField] private VoidEvent onPauseStart;
    [SerializeField] private VoidEvent onPauseEnd;

    [SerializeField] private VoidEvent OnPlayerDeath;

    void Update()
    {
        currentState.onUpdateState?.Invoke();
    }

    public void ChangeGameState(GameState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState();
            lastState = currentState;
        }

        currentState = newState;
        currentState.EnterState();
    }

    public void SwitchToGameplay()
    {
        ChangeGameState(gameplayState);
    }

    public void SwitchToMenu()
    {
        ChangeGameState(mainMenu);
    }

    public void SwitchToPause()
    {
        ChangeGameState(pauseState);
    }

    private void SwitchToGameOver()
    {
        ChangeGameState(gameOverState);
    }

    public void SwitchToLastState()
    {
        ChangeGameState(lastState);
    }

    private void OnEnable()
    {
        onMainMenu.gameEvent += SwitchToMenu;
        onGameplay.gameEvent += SwitchToGameplay;

        onPauseStart.gameEvent += SwitchToPause;
        onPauseEnd.gameEvent += SwitchToLastState;

        OnPlayerDeath.gameEvent += SwitchToGameOver;
    }

    private void OnDisable()
    {
        onMainMenu.gameEvent -= SwitchToMenu;
        onGameplay.gameEvent -= SwitchToGameplay;

        onPauseStart.gameEvent -= SwitchToPause;
        onPauseEnd.gameEvent -= SwitchToLastState;


        OnPlayerDeath.gameEvent -= SwitchToGameOver;
    }

}
