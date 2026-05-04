using System.Collections;
using UnityEngine;
using UnityEngine.Audio;


[RequireComponent(typeof(AudioSource))]
public class MenuMusicManager : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip ambientTrack;         
    public AudioMixer audioMixer;           
    public string mixerVolumeParam = "MusicVolume"; 

    [Header("Fade In")]
    public float fadeInDuration = 2.5f;      
    public float targetVolume = 0.75f;     

    [Header("Low-Pass Filter (horror feel)")]
    public bool useLowPass = true;
    public float lowPassCutoff = 3500f;  
    public float lowPassResonance = 1.2f;

    [Header("Mute Toggle")]
    public bool startMuted = false;

    
    AudioSource _source;
    AudioLowPassFilter _lpf;
    bool _muted = false;
    float _savedVolume;
    Coroutine _fadeCoroutine;

    void Awake()
    {
        _source = GetComponent<AudioSource>();

        
        _source.clip = ambientTrack;
        _source.loop = true;
        _source.playOnAwake = false;
        _source.volume = 0f;            

        
        if (useLowPass)
        {
            _lpf = gameObject.AddComponent<AudioLowPassFilter>();
            _lpf.cutoffFrequency = lowPassCutoff;
            _lpf.lowpassResonanceQ = lowPassResonance;
        }

        if (startMuted)
        {
            _muted = true;
            _source.volume = 0f;
        }
    }

    void Start()
    {
        if (ambientTrack == null)
        {
            Debug.LogWarning("MenuMusicManager: No AudioClip assigned.");
            return;
        }

        _source.Play();

        if (!_muted)
            _fadeCoroutine = StartCoroutine(FadeIn());
    }

    

    
    public void ToggleMute()
    {
        _muted = !_muted;

        if (_fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine);

        float targetVol = _muted ? 0f : targetVolume;
        _fadeCoroutine = StartCoroutine(FadeTo(targetVol, 0.4f));
    }

    
    public void FadeOut(float duration = 1.5f)
    {
        if (_fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine);
        _fadeCoroutine = StartCoroutine(FadeTo(0f, duration));
    }

    

    IEnumerator FadeIn()
    {
        float elapsed = 0f;
        while (elapsed < fadeInDuration)
        {
            elapsed += Time.deltaTime;
            _source.volume = Mathf.Lerp(0f, targetVolume, elapsed / fadeInDuration);
            yield return null;
        }
        _source.volume = targetVolume;
    }

    IEnumerator FadeTo(float target, float duration)
    {
        float start = _source.volume;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            _source.volume = Mathf.Lerp(start, target, elapsed / duration);
            yield return null;
        }
        _source.volume = target;
    }
}
