using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BatteryGlowCone : MonoBehaviour
{
    public Light2D glowLight;
    public Transform flashlight;
    public float maxDistance = 5f;
    public float coneAngle = 60f;

    void Update()
    {
        Vector2 toBattery = transform.position - flashlight.position;

        float distance = toBattery.magnitude;

        float angle = Vector2.Angle(flashlight.up, toBattery);

        bool insideDistance = distance <= maxDistance;
        bool insideCone = angle <= coneAngle / 2f;

        glowLight.enabled = insideDistance && insideCone;
    }
}