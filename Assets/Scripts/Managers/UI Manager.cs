using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameplayMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;

    [SerializeField] GameState gameplayState;
    [SerializeField] GameState mainMenuState;
    [SerializeField] GameState pauseState;
    [SerializeField] GameState gameOverState;

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

    public void DisableAllMenus()
    {
        mainMenu.SetActive(false);
        gameplayMenu.SetActive(false);
        pauseMenu.SetActive(false);
        gameOverMenu.SetActive(false);
    }

    private void OnEnable()
    {
        mainMenuState.onEnterState += EnableMainMenu;
        gameplayState.onEnterState += EnableGameplay;
        pauseState.onEnterState += EnablePauseMenu;
        gameOverState.onEnterState += EnableGameOverMenu;

        OnGameplay.gameEvent += EnableGameplay;
        OnMainMenu.gameEvent += EnableMainMenu;
    }

    private void OnDisable()
    {
        mainMenuState.onEnterState -= EnableMainMenu;
        gameplayState.onEnterState -= EnableGameplay;
        pauseState.onEnterState -= EnablePauseMenu;
        gameOverState.onEnterState -= EnableGameOverMenu;

        OnGameplay.gameEvent -= EnableGameplay;
        OnMainMenu.gameEvent -= EnableMainMenu;
    }
}
