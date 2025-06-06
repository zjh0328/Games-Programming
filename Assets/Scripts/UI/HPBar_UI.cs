using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HPBar_UI : MonoBehaviour
{
    private Entity entity;
    private RectTransform barTransform;
    private Slider slider;
    private Image fillImage;
    private CharacterStats myStats;


    [Header("Color Settings")]
    [SerializeField] private Color healthyColor = Color.green;
    [SerializeField] private Color mediumColor = Color.yellow;
    [SerializeField] private Color lowColor = Color.red;
    
    private bool isFlipped = false;

    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
        barTransform = GetComponent<RectTransform>();
        slider = GetComponentInChildren<Slider>();
        fillImage = slider.fillRect.GetComponent<Image>();
        myStats = GetComponentInParent<CharacterStats>();
    }

    private void OnEnable()
    {
        if (entity != null) entity.onFlipped += FlipUI;
        if (myStats != null) myStats.onHealthChanged += UpdateHPUI;
    }

    private void OnDisable()
    {
        if (entity != null) entity.onFlipped -= FlipUI;
        if (myStats != null) myStats.onHealthChanged -= UpdateHPUI;
    }

    private void Start()
    {
        UpdateHPUI();
    }

    private void UpdateHPUI()
    {
        slider.maxValue = myStats.getMaxHP();
        slider.value = myStats.currentHP;
        UpdateColor();
    }

    private void UpdateColor()
    {
        float percent = slider.value / slider.maxValue;
        if (percent > 0.5f)
            fillImage.color = healthyColor;
        else if (percent > 0.2f)
            fillImage.color = mediumColor;
        else
            fillImage.color = lowColor;
    }

    private void FlipUI()
    {
        isFlipped = !isFlipped;
        barTransform.localScale = new Vector3(isFlipped ? -1 : 1, 1, 1);
    }
}
