using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameplayMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private GameObject winnerMenu;

    [Header("Game states")]
    [SerializeField] GameState gameplayState;
    [SerializeField] GameState mainMenuState;
    [SerializeField] GameState pauseState;
    [SerializeField] GameState gameOverState;
    [SerializeField] GameState winnerState;

    [Header("Events")]
    [SerializeField] VoidEvent OnGameplay;
    [SerializeField] VoidEvent OnMainMenu;

    public void EnableMainMenu()
    {
        DisableAllMenus();
        mainMenu.SetActive(true);
    }

    public void EnableGameplay()
    {
        DisableAllMenus();
        gameplayMenu.SetActive(true);
    }

    public void EnablePauseMenu()
    {
        DisableAllMenus();
        pauseMenu.SetActive(true);
    }

    public void EnableGameOverMenu()
    {
        DisableAllMenus();
        gameOverMenu.SetActive(true);
    }
    public void EnableWinnerMenu()
    {
        DisableAllMenus();
        winnerMenu.SetActive(true);
    }

    public void DisableAllMenus()
    {
        mainMenu.SetActive(false);
        gameplayMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
        winnerMenu.SetActive(false);
    }

    private void OnEnable()
    {
        mainMenuState.onEnterState += EnableMainMenu;
        gameplayState.onEnterState += EnableGameplay;
        pauseState.onEnterState += EnablePauseMenu;
        gameOverState.onEnterState += EnableGameOverMenu;
        winnerState.onEnterState += EnableWinnerMenu;

        OnGameplay.gameEvent += EnableGameplay;
        OnMainMenu.gameEvent += EnableMainMenu;
    }

    private void OnDisable()
    {
        mainMenuState.onEnterState -= EnableMainMenu;
        gameplayState.onEnterState -= EnableGameplay;
        pauseState.onEnterState -= EnablePauseMenu;
        gameOverState.onEnterState -= EnableGameOverMenu;
        winnerState.onEnterState -= EnableWinnerMenu;

        OnGameplay.gameEvent -= EnableGameplay;
        OnMainMenu.gameEvent -= EnableMainMenu;
    }
}
