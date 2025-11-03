using UnityEngine;

public class EnvironmentShake : MonoBehaviour
{
    [SerializeField] private float maxMagnitude = 0.5f;
    [SerializeField] private float frequency = 8f;

    private Vector3 originalPos;

    private void Start()
    {
        originalPos = transform.position;
    }

    private void Update()
    {
        if (EarthquakeController.Instance == null || !EarthquakeController.Instance.IsQuaking)
            return;

        float intensity = EarthquakeController.Instance.Intensity;
        float time = Time.time * frequency;

        float x = (Mathf.PerlinNoise(time, 0f) - 0.5f) * 2f * intensity * maxMagnitude;
        float z = (Mathf.PerlinNoise(0f, time) - 0.5f) * 2f * intensity * maxMagnitude;

        transform.position = originalPos + new Vector3(x, 0f, z);
    }
}