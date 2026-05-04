using UnityEngine;
using UnityEngine.UI;

public class SmallJumpscare : MonoBehaviour
{
    public GameObject jumpscareImage;
    public AudioSource jumpscareSound;
    public float scareTime = 0.8f;

    public Slider batteryBar; // 👈 add this

    private bool used = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (used) return;

        if (other.CompareTag("Player"))
        {
            used = true;

            // 🔥 hide battery bar
            if (batteryBar != null)
                batteryBar.gameObject.SetActive(false);

            jumpscareImage.SetActive(true);
            jumpscareSound.Play();

            Invoke(nameof(HideJumpscare), scareTime);
        }
    }

    void HideJumpscare()
    {
        jumpscareImage.SetActive(false);

        if (batteryBar != null)
            batteryBar.gameObject.SetActive(true);
    }
}