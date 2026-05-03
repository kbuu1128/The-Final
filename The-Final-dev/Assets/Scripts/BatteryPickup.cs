using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    public float amount = 30f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FlashlightSystem flashlight = other.GetComponent<FlashlightSystem>();

            if (flashlight != null)
            {
                flashlight.AddBattery(amount);
                Destroy(gameObject);
            }
        }
    }
}