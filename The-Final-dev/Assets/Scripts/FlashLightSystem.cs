using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class FlashlightSystem : MonoBehaviour
{
    public float battery = 100f;
    public float drainRate = 4f;

    public GameObject flashlightLight;
    public GameObject bigJumpscareImage;
    public AudioSource jumpscareSound;

    private bool dead = false;

    void Update()
    {
        if (dead) return;

        battery -= drainRate * Time.deltaTime;

        if (battery <= 0)
        {
            battery = 0;
            StartCoroutine(DeathJumpscare());
        }
    }

    public void AddBattery(float amount)
    {
        battery += amount;
        if (battery > 100) battery = 100;
    }

    IEnumerator DeathJumpscare()
    {
        dead = true;
        flashlightLight.SetActive(false);
        bigJumpscareImage.SetActive(true);
        jumpscareSound.Play();

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}