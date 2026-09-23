using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

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
}