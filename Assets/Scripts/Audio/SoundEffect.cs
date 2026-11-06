using UnityEngine;

[System.Serializable]
public class SoundEffect
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField, Range(0f, 1f)] private float volume = 1f;
    [SerializeField] private Vector2 pitchRange = new Vector2(0.95f, 1.05f);

    public void Play()
    {
        if (clips == null || clips.Length == 0)
            return;

        AudioClip clip = clips[Random.Range(0, clips.Length)];
        float pitch = Random.Range(pitchRange.x, pitchRange.y);

        SoundPlayer.Play(clip, volume, pitch);
    }
}