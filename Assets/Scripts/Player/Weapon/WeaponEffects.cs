using UnityEngine;

public class WeaponEffects : MonoBehaviour
{
    [SerializeField] private Transform muzzle;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private LineRenderer tracer;
    [SerializeField] private ParticleSystem impactPrefab;
    [SerializeField, Min(0f)] private float tracerDuration = 0.05f;
    [SerializeField, Min(0f)] private float impactLifetime = 1f;

    private float tracerEndTime;

    private void Awake()
    {
        tracer.enabled = false;
    }

    private void Update()
    {
        if (tracer.enabled && Time.time >= tracerEndTime)
            tracer.enabled = false;
    }

    public void PlayShot(Vector3 endPoint)
    {
        muzzleFlash.Play();

        tracer.SetPosition(0, muzzle.position);
        tracer.SetPosition(1, endPoint);
        tracer.enabled = true;
        tracerEndTime = Time.time + tracerDuration;
    }

    public void PlayImpact(Vector3 point, Vector3 normal)
    {
        ParticleSystem impact = Instantiate(
            impactPrefab,
            point,
            Quaternion.LookRotation(normal));

        Destroy(impact.gameObject, impactLifetime);
    }
}