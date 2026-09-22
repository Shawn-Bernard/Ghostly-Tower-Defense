using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-100)]
public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance => instance;

    [SerializeField] private GameStateManager gameStateManager;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private LevelManager levelManager;
    public GameStateManager GameStateManager => gameStateManager;
    public UIManager UIManager => uiManager;

    public LevelManager LevelManager => levelManager;

    [SerializeField] private PlayerInputActions playerAction;

    [SerializeField] private GameState gameplayState;
    [SerializeField] private GameState pauseState;
    [SerializeField] private GameState gameOverState;

    [SerializeField] private VoidEvent onPause;
    [SerializeField] private VoidEvent onPauseEnd;

    void Start()
    {
        gameStateManager ??= GetComponentInChildren<GameStateManager>();
        uiManager ??= GetComponentInChildren<UIManager>();
    }

    

    public virtual void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void StopGameTime()
    {
        Time.timeScale = 0;
    }
    public void ResumeGameTime()
    {
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        if (gameplayState != null)
        {
            gameplayState.onEnterState += ResumeGameTime;
        }
        if (pauseState != null)
        {
            pauseState.onEnterState += StopGameTime;
        }

        if (gameOverState != null)
        {
            gameOverState.onEnterState += StopGameTime;
            gameOverState.onExitState += ResumeGameTime;
        }
    }

    private void OnDisable()
    {
        if (gameplayState != null)
        {
            gameplayState.onEnterState -= ResumeGameTime;
        }
        if (pauseState != null)
        {
            pauseState.onEnterState -= StopGameTime;
        }
        if (gameOverState != null)
        {
            gameOverState.onEnterState -= StopGameTime;
            gameOverState.onExitState -= ResumeGameTime;
        }
    }
}
