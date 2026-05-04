using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class FlashlightSystem : MonoBehaviour
{
    public float battery = 100f;
    public float drainRate = 4f;

    public GameObject flashlightLight;
    public GameObject bigJumpscareImage;
    public AudioSource jumpscareSound;

    public Slider batteryBar;

    private bool dead = false;

    void Start()
    {
        battery = Mathf.Clamp(battery, 0, 100);

        if (batteryBar != null)
        {
            batteryBar.minValue = 0;
            batteryBar.maxValue = 100;
            batteryBar.value = battery;
        }

        if (bigJumpscareImage != null)
        {
            bigJumpscareImage.SetActive(false);
        }
    }

    void Update()
    {
        if (dead) return;

        battery -= drainRate * Time.deltaTime;
        battery = Mathf.Clamp(battery, 0, 100);

        UpdateBatteryBar();

        if (battery <= 0)
        {
            StartCoroutine(DeathJumpscare());
        }
    }

    public void AddBattery(float amount)
    {
        if (dead) return;

        battery += amount;
        battery = Mathf.Clamp(battery, 0, 100);

        UpdateBatteryBar();

        if (flashlightLight != null && battery > 0)
        {
            flashlightLight.SetActive(true);
        }
    }

    void UpdateBatteryBar()
    {
        if (batteryBar != null)
        {
            batteryBar.value = battery;
        }
    }

    IEnumerator DeathJumpscare()
    {
        dead = true;

        if (flashlightLight != null)
        {
            flashlightLight.SetActive(false);
        }

        if (bigJumpscareImage != null)
        {
            bigJumpscareImage.SetActive(true);
        }

        // ?? HIDE UI BAR
        if (batteryBar != null)
        {
            batteryBar.gameObject.SetActive(false);
        }

        if (jumpscareSound != null)
        {
            jumpscareSound.Play();
        }

        yield return new WaitForSeconds(6f);

        PlayerPrefs.SetString("RestartLevel", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("GameOver");
    }
}
