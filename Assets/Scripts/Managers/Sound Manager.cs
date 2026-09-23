using UnityEngine;

[CreateAssetMenu(menuName = "Sound Manager", fileName = "Sound Manager")]
public class SoundManager : ScriptableObject
{
    private static SoundManager instance;

    public static SoundManager Instance
    {
        get 
        { 
            if (instance == null)
            {
                instance = Resources.Load<SoundManager>("Sound Manager");
            }
            return instance; 
        }
    }
    public AudioSource soundObject;

    private static float volumeChangeMultiplier = 0.15f;
    private static float pitchChangeMultiplier = 0.1f;

    public static void PlaySoundClip(AudioClip clip, Vector2 soundPosition,float volume)
    {
        float randomVolume = Random.Range(volume - volumeChangeMultiplier, volume + volumeChangeMultiplier);
        float randomPitch = Random.Range(1 - pitchChangeMultiplier, 1 + pitchChangeMultiplier);

        AudioSource audioSource = Instantiate(instance.soundObject, soundPosition, Quaternion.identity);

        audioSource.clip = clip;
        audioSource.volume = randomVolume;
        audioSource.pitch = randomPitch;
        audioSource.Play();
    }

    public static void PlaySoundClip(AudioClip[] clips, Vector2 soundPosition, float volume)
    {
        int randomClip = Random.Range(0, clips.Length);
        float randomVolume = Random.Range(volume - volumeChangeMultiplier, volume + volumeChangeMultiplier);
        float randomPitch = Random.Range(1 - pitchChangeMultiplier, 1 + pitchChangeMultiplier);

        AudioSource audioSource = Instantiate(instance.soundObject, soundPosition, Quaternion.identity);

        audioSource.clip = clips[randomClip];
        audioSource.volume = randomVolume;
        audioSource.pitch = randomPitch;
        audioSource.Play();
    }
}
