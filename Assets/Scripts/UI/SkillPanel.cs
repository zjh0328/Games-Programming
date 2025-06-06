using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanel : MonoBehaviour
{
    public static SkillPanel instance;

    public GameObject DashIcon;
    public GameObject FireballIcon;
    public GameObject BlackholeIcon;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        ShowAll();
    }

    private void Start()
    {
        HideAll();       
        ShowAll();        
        UpdateText();   
    }

    public void UpdateText()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform?.GetChild(i)?.GetComponentInChildren<SkillTextSize>()?.UpdateSize();
        }
    }

    private void HideAll()
    {
        DashIcon.SetActive(false);
        FireballIcon.SetActive(false);
        BlackholeIcon.SetActive(false);
    }

    public void ShowAll()
    {
        DashIcon.SetActive(true);
        FireballIcon.SetActive(true);
        BlackholeIcon.SetActive(true);
    }
}
