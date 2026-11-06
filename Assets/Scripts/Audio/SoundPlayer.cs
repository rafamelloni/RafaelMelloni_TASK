using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private const int PoolSize = 8;

    private static SoundPlayer instance;

    private AudioSource[] sources;
    private int nextSourceIndex;

    public static void Play(AudioClip clip, float volume, float pitch)
    {
        if (instance == null)
            CreateInstance();

        AudioSource source = instance.GetNextSource();

        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
    }

    private static void CreateInstance()
    {
        GameObject playerObject = new GameObject("SoundPlayer");
        DontDestroyOnLoad(playerObject);

        instance = playerObject.AddComponent<SoundPlayer>();
        instance.CreateSources();
    }

    private void CreateSources()
    {
        sources = new AudioSource[PoolSize];

        for (int i = 0; i < PoolSize; i++)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.playOnAwake = false;
            source.spatialBlend = 0f;
            sources[i] = source;
        }
    }

    private AudioSource GetNextSource()
    {
        AudioSource source = sources[nextSourceIndex];
        nextSourceIndex = (nextSourceIndex + 1) % sources.Length;
        return source;
    }
}