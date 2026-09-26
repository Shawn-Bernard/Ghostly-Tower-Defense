using NUnit.Framework.Constraints;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource backgroundMusic;

    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip gameplayMusic;
    [SerializeField] private AudioClip endGameMusic;

    [Range(0,1)]
    [SerializeField] private float backGroundMusicVolume;

    [SerializeField] private GameState mainMenu;
    [SerializeField] private GameState gameplayState;
    [SerializeField] private GameState gameOverState;
    [SerializeField] private GameState winnerState;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        backgroundMusic ??= GetComponent<AudioSource>();
    }

    public void SwitchBackgroundMusic(AudioClip musicClip)
    {
        backgroundMusic.clip = musicClip;
        backgroundMusic.loop = true;
        backgroundMusic.volume = backGroundMusicVolume;
        backgroundMusic.Play();
    }

    public void PlayMainMenu()
    {
        SwitchBackgroundMusic(mainMenuMusic);
    }

    public void PlayGameplay()
    {
        SwitchBackgroundMusic(gameplayMusic);
    }

    public void PlayEndGame()
    {
        SwitchBackgroundMusic(endGameMusic);
    }

    private void OnEnable()
    {
        mainMenu.onEnterState += PlayMainMenu;
        gameplayState.onEnterState += PlayGameplay;
        gameOverState.onEnterState += PlayEndGame;
        winnerState.onEnterState += PlayEndGame;
    }

    private void OnDisable()
    {
        mainMenu.onEnterState -= PlayMainMenu;
        gameplayState.onEnterState -= PlayGameplay;
        gameOverState.onEnterState -= PlayEndGame;
        winnerState.onEnterState -= PlayEndGame;
    }

    #region SFX Logic
    /// <summary>
    /// Plays a audio clip at a position with volume
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="position"></param>
    /// <param name="volume"></param>
    public void PlaySound(AudioClip clip, Vector2 position, float volume)
    {
        if (clip == null) return;

        Vector3 spawnPosition = position;

        AudioSource.PlayClipAtPoint(clip, spawnPosition, volume);
    }
    /// <summary>
    /// Plays a random audio clip from a array of clips at a position, with volume
    /// </summary>
    /// <param name="clips"></param>
    /// <param name="position"></param>
    /// <param name="volume"></param>
    public void PlayRandomSound(AudioClip[] clips, Vector2 position, float volume)
    {
        if (clips == null || clips.Length == 0) return;

        AudioClip clip = clips[Random.Range(0, clips.Length)];

        Vector3 spawnPosition = position;

        AudioSource.PlayClipAtPoint(clip, spawnPosition, volume);
    }
    #endregion
}