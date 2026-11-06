using TMPro;
using UnityEngine;

public class ShootingGallery : MonoBehaviour
{
    private enum State
    {
        Ready,
        Running,
        Finished
    }

    [SerializeField] private TMP_Text timerText;
    [SerializeField, Min(1f)] private float roundDuration = 90f;
    [SerializeField, Min(0f)] private float resetDelay = 5f;
    [SerializeField] private SoundEffect winSound = new SoundEffect();

    private GalleryTarget[] targets;
    private State state = State.Ready;
    private float timeLeft;
    private float resetTime;

    public bool CanShootTargets => state != State.Finished;

    private void Awake()
    {
        targets = GetComponentsInChildren<GalleryTarget>();
        ShowTime(roundDuration);
    }

    private void Update()
    {
        if (state == State.Running)
        {
            timeLeft -= Time.deltaTime;

            if (timeLeft <= 0f)
                Finish(false);
            else
                ShowTime(timeLeft);
        }
        else if (state == State.Finished && Time.time >= resetTime)
        {
            ResetGallery();
        }
    }

    public void RegisterTargetDown()
    {
        if (state == State.Ready)
        {
            state = State.Running;
            timeLeft = roundDuration;
        }

        if (AreAllTargetsDown())
            Finish(true);
    }

    private bool AreAllTargetsDown()
    {
        foreach (GalleryTarget target in targets)
        {
            if (!target.IsDown)
                return false;
        }

        return true;
    }

    private void Finish(bool won)
    {
        state = State.Finished;
        timerText.text = won ? "VICTORY" : "DEFEAT";
        resetTime = Time.time + resetDelay;

        if (won)
            winSound.Play();
    }

    private void ResetGallery()
    {
        foreach (GalleryTarget target in targets)
            target.ResetTarget();

        state = State.Ready;
        ShowTime(roundDuration);
    }

    private void ShowTime(float seconds)
    {
        int totalSeconds = Mathf.CeilToInt(seconds);
        timerText.text = $"{totalSeconds / 60}:{totalSeconds % 60:00}";
    }
}