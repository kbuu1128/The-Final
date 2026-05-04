using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;


[RequireComponent(typeof(Button))]
public class HorrorMenuButton : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler,
    IPointerClickHandler
{
    [Header("References")]
    public TextMeshProUGUI label;        
    public Image background;  

    [Header("Colors")]
    public Color normalColor = new Color(0.78f, 0.63f, 0.56f, 1f); 
    public Color hoverColor = new Color(1.00f, 0.60f, 0.53f, 1f); 
    public Color selectedColor = new Color(1.00f, 0.40f, 0.27f, 1f); 
    public Color bgNormal = new Color(0f, 0f, 0f, 0f);
    public Color bgHover = new Color(0.39f, 0.04f, 0f, 0.25f);
    public Color bgSelected = new Color(0.31f, 0f, 0f, 0.30f);

    [Header("Glow (TMP material must have Glow enabled)")]
    public bool useGlow = true;
    public float glowNormal = 0f;
    public float glowHover = 0.3f;
    public float glowSelected = 0.55f;
    public Color glowColor = new Color(0.78f, 0.24f, 0.16f, 1f);

    [Header("Letter Spacing")]
    public float spacingNormal = 30f;   
    public float spacingHover = 40f;
    public float spacingSelected = 35f;

    [Header("Transition")]
    public float transitionSpeed = 8f;    

    
    static HorrorMenuButton currentSelected;

    bool _hovered = false;
    bool _selected = false;

    
    Color _targetLabelColor;
    Color _targetBgColor;
    float _targetGlow;
    float _targetSpacing;

    
    Color _curLabelColor;
    Color _curBgColor;
    float _curGlow;
    float _curSpacing;

    Material _matInstance; 

    
    void Awake()
    {
        if (label == null)
            label = GetComponentInChildren<TextMeshProUGUI>();

        
        if (label != null && useGlow)
        {
            _matInstance = new Material(label.fontMaterial);
            label.fontMaterial = _matInstance;
        }

        ApplyState(instant: true);
    }

    void Update()
    {
        ResolveTargets();
        Interpolate();
        PushToUI();
    }

    
    public void OnPointerEnter(PointerEventData _) { _hovered = true; }
    public void OnPointerExit(PointerEventData _) { _hovered = false; }

    public void OnPointerClick(PointerEventData _)
    {
        if (currentSelected != null && currentSelected != this)
            currentSelected.Deselect();

        _selected = true;
        currentSelected = this;
    }

    public void Deselect()
    {
        _selected = false;
    }

    
    void ResolveTargets()
    {
        if (_selected)
        {
            _targetLabelColor = selectedColor;
            _targetBgColor = bgSelected;
            _targetGlow = glowSelected;
            _targetSpacing = spacingSelected;
        }
        else if (_hovered)
        {
            _targetLabelColor = hoverColor;
            _targetBgColor = bgHover;
            _targetGlow = glowHover;
            _targetSpacing = spacingHover;
        }
        else
        {
            _targetLabelColor = normalColor;
            _targetBgColor = bgNormal;
            _targetGlow = glowNormal;
            _targetSpacing = spacingNormal;
        }
    }

    void Interpolate()
    {
        float t = transitionSpeed * Time.deltaTime;
        _curLabelColor = Color.Lerp(_curLabelColor, _targetLabelColor, t);
        _curBgColor = Color.Lerp(_curBgColor, _targetBgColor, t);
        _curGlow = Mathf.Lerp(_curGlow, _targetGlow, t);
        _curSpacing = Mathf.Lerp(_curSpacing, _targetSpacing, t);
    }

    void PushToUI()
    {
        if (label != null)
        {
            label.color = _curLabelColor;
            label.characterSpacing = _curSpacing;
        }

        if (background != null)
            background.color = _curBgColor;

        if (useGlow && _matInstance != null)
        {
            _matInstance.SetFloat(ShaderUtilities.ID_GlowPower, _curGlow);
            _matInstance.SetColor(ShaderUtilities.ID_GlowColor, glowColor);
        }
    }

    void ApplyState(bool instant)
    {
        ResolveTargets();
        if (instant)
        {
            _curLabelColor = _targetLabelColor;
            _curBgColor = _targetBgColor;
            _curGlow = _targetGlow;
            _curSpacing = _targetSpacing;
        }
        PushToUI();
    }
}
