using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameplayMenu;
    [SerializeField] private GameObject pauseMenu;

    [SerializeField] GameState gameplayState;
    [SerializeField] GameState mainMenuState;
    [SerializeField] GameState pauseMenuState;
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

    public void DisableAllMenus()
    {
        mainMenu.SetActive(false);
        gameplayMenu.SetActive(false);
        pauseMenu.SetActive(false);
    }

    private void OnEnable()
    {
        mainMenuState.onEnterState += EnableMainMenu;
        gameplayState.onEnterState += EnableGameplay;
        pauseMenuState.onEnterState += EnablePauseMenu;
    }

    private void OnDisable()
    {
        mainMenuState.onEnterState -= EnableMainMenu;
        gameplayState.onEnterState -= EnableGameplay;
        pauseMenuState.onEnterState -= EnablePauseMenu;
    }
}
