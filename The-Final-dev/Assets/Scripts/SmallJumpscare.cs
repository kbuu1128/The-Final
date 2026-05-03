using UnityEngine;

public class SmallJumpscare : MonoBehaviour
{
    public GameObject jumpscareImage;
    public AudioSource jumpscareSound;
    public float scareTime = 0.8f;

    private bool used = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (used) return;

        if (other.CompareTag("Player"))
        {
            used = true;
            jumpscareImage.SetActive(true);
            jumpscareSound.Play();

            Invoke(nameof(HideJumpscare), scareTime);
        }
    }

    void HideJumpscare()
    {
        jumpscareImage.SetActive(false);
    }
}