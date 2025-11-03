using UnityEngine;

public class CameraShaking : MonoBehaviour
{
    [SerializeField] private float maxRotation = 1.2f;
    [SerializeField] private float maxOffset = 0.3f;
    [SerializeField] private float frequency = 12f;

    private Vector3 originalPos;
    private Quaternion originalRot;

    private void Start()
    {
        originalPos = transform.localPosition;
        originalRot = transform.localRotation;
    }

    private void Update()
    {
        if (EarthquakeController.Instance == null || !EarthquakeController.Instance.IsQuaking)
            return;

        float intensity = EarthquakeController.Instance.Intensity;
        float time = Time.time * frequency;

        // Position shake
        float offsetX = (Mathf.PerlinNoise(time, 0f) - 0.5f) * 2f * maxOffset * intensity;
        float offsetY = (Mathf.PerlinNoise(0f, time) - 0.5f) * 2f * maxOffset * 0.5f * intensity;
        float offsetZ = 0f;

        // Rotation shake
        float rotX = Mathf.Sin(time * 1.1f) * maxRotation * intensity;
        float rotZ = Mathf.Cos(time * 1.3f) * maxRotation * intensity;

        transform.localPosition = originalPos + new Vector3(offsetX, offsetY, offsetZ);
        transform.localRotation = originalRot * Quaternion.Euler(rotX, 0f, rotZ);
    }
}
