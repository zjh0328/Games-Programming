using TMPro;
using UnityEngine;

public class SkillTextSize : MonoBehaviour
{
    [SerializeField] private int fontSize = 26;

    private void Start()
    {
        UpdateSize();
    }

    public void UpdateSize()
    {
        var textComponent = GetComponent<TextMeshProUGUI>();
        textComponent.fontSize = fontSize;
    }
}
