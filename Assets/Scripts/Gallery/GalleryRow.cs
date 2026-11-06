using UnityEngine;

public class GalleryRow : MonoBehaviour
{
    [SerializeField, Min(0f)] private float speed = 2f;
    [SerializeField, Min(0f)] private float range = 3f;
    [SerializeField] private bool startsRight;

    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.localPosition;
    }

    private void Update()
    {
        float offset = Mathf.PingPong(Time.time * speed, range * 2f) - range;

        if (startsRight)
            offset = -offset;

        transform.localPosition = startPosition + Vector3.right * offset;
    }
}