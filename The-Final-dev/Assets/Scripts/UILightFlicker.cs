using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class UILightFlicker : MonoBehaviour
{
    [Header("The UI Image that acts as the light cone")]
    public Image lightImage;          

    [Header("Bulb sprite (small dot at top of screen)")]
    public Image bulbImage;           

    [Header("Light Colors")]
    public Color brightColor = new Color(0.96f, 0.87f, 0.48f, 0.18f); 
    public Color dimColor = new Color(0.10f, 0.08f, 0.04f, 0.05f); 

    [Header("Bulb dot colors")]
    public Color bulbBright = new Color(0.98f, 0.90f, 0.55f, 1.00f);
    public Color bulbDim = new Color(0.25f, 0.18f, 0.05f, 1.00f);

    [Header("Flicker Settings")]
    public float flickerInterval = 0.08f;
    public float burstChance = 0.15f;
    public int burstCount = 3;
    public float burstSpeed = 0.04f;

    void Start()
    {
        if (lightImage == null)
            lightImage = GetComponent<Image>();

        StartCoroutine(FlickerRoutine());
    }

    IEnumerator FlickerRoutine()
    {
        while (true)
        {
            if (Random.value < burstChance)
            {
                for (int i = 0; i < burstCount; i++)
                {
                    SetLevel(Random.value < 0.5f ? 0f : 1f);
                    yield return new WaitForSeconds(burstSpeed);
                }
            }
            else
            {
                float roll = Random.value;
                if (roll < 0.12f) SetLevel(0f);
                else if (roll < 0.20f) SetLevel(Random.Range(0f, 0.4f));
                else SetLevel(Random.Range(0.75f, 1f));
            }

            float wait = flickerInterval + Random.Range(-0.03f, 0.05f);
            yield return new WaitForSeconds(wait);
        }
    }

    void SetLevel(float t)
    {
        if (lightImage != null)
            lightImage.color = Color.Lerp(dimColor, brightColor, t);

        if (bulbImage != null)
            bulbImage.color = Color.Lerp(bulbDim, bulbBright, t);
    }
}
