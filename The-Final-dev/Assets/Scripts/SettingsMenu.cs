using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SettingsMenu : MonoBehaviour
{
    [Header("Panel Reference")]
    public GameObject settingsPanel;    

    [Header("UI Controls")]
    public Slider volumeSlider;     
    public Toggle fullscreenToggle; 
    public TMP_Text volumeLabel;      

    void Start()
    {
       
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 0.75f);
        bool savedFullscreen = PlayerPrefs.GetInt("Fullscreen", 1) == 1;

        
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }

        if (fullscreenToggle != null)
        {
            fullscreenToggle.isOn = savedFullscreen;
            fullscreenToggle.onValueChanged.AddListener(OnFullscreenChanged);
        }

        
        AudioListener.volume = savedVolume;
        Screen.fullScreen = savedFullscreen;

        
        settingsPanel.SetActive(false);
    }

    
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        PlayerPrefs.Save(); 
    }

    
    void OnVolumeChanged(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat("MasterVolume", value);

        if (volumeLabel != null)
            volumeLabel.text = Mathf.RoundToInt(value * 100) + "%";
    }

    
    void OnFullscreenChanged(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);
    }
}