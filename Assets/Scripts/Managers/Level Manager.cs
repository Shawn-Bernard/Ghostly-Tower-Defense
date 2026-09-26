using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private string spawnPointName;

    private GameObject spawnPoint;

    [Header("Action Events")]
    [SerializeField] private VoidEvent onMainMenu;
    [SerializeField] private VoidEvent onGameplay;

    
    private Vector3 spawnPosition;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void LoadSceneWithSpawnPoint(string sceneName, string triggerSpawnPoint)
    {
        
        spawnPointName = triggerSpawnPoint;

        SceneManager.LoadScene(sceneName);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    private void SetPlayerSpawnPoint(string spawnPointName)
    {
        if (spawnPointName != null)
        {
            spawnPoint = GameObject.Find(spawnPointName);
        }

        if (spawnPoint != null)
        {
            spawnPosition = spawnPoint.transform.position;
        }
        else
        {
            Debug.Log("No spawn point");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int SceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (SceneIndex == 0)// Bootloader Scene we dont want to be in there
        {
            LoadMainMenu();
        }
        if (SceneIndex == 1)// Main menu
        {
            onMainMenu.RaiseEvent();
        }
        if (SceneIndex > 1)// This would be game/levels
        {
            onGameplay.RaiseEvent();
            SetPlayerSpawnPoint(spawnPointName);
        }
        
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void LoadMainMenu()
    {
        onMainMenu.RaiseEvent();
        LoadScene("Main Menu");
    }

    public void LoadGameplay()
    {
        LoadScene("Level 1");
    }

    public void LoadNextLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(sceneIndex + 1);
        }
        else
        {
            Debug.Log("No more levels!");
            LoadMainMenu();
        }
    }
    public void LoadLastLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneIndex - 1 >= 0)
        {
            SceneManager.LoadScene(sceneIndex - 1);
        }
        else
        {
            Debug.Log("No previous level!");
            LoadMainMenu();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
