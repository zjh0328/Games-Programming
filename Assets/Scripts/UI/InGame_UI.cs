using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGame_UI : MonoBehaviour
{
    public static InGame_UI instance;

    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider slider;

    [Header("Skill Icons")]
    [SerializeField] private Image DashImage;
    [SerializeField] private Image FireballImage;
    [SerializeField] private Image BlackholeImage;

    private SkillManager skill;
    private Player player;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); 
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        if (playerStats != null)
        {
            playerStats.onHealthChanged += UpdateHPUI;
        }

        skill = SkillManager.instance;
        player = PlayerManager.instance?.player;

        UpdateHPUI();
    }

    private void Update()
    {
        if (skill == null) return;

        if (Input.GetKeyDown(KeyCode.LeftShift)) SetSkillCooldownImage(DashImage);
        if (Input.GetKeyDown(KeyCode.R)) SetSkillCooldownImage(BlackholeImage);
        if (Input.GetKeyDown(KeyCode.F)) SetSkillCooldownImage(FireballImage);

        FillSkillCooldownImage(DashImage, skill.Dash.cooldown);
        FillSkillCooldownImage(FireballImage, skill.Fireball.cooldown);
        FillSkillCooldownImage(BlackholeImage, skill.Blackhole.cooldown);
    }

    private void UpdateHPUI()
    {
        if (slider == null || playerStats == null) return;

        slider.maxValue = playerStats.getMaxHP();
        slider.value = playerStats.currentHP;
    }

    private void SetSkillCooldownImage(Image skillImage)
    {
        if (skillImage != null && skillImage.fillAmount <= 0)
        {
            skillImage.fillAmount = 1f;
        }
    }

    private void FillSkillCooldownImage(Image skillImage, float cooldown)
    {
        if (skillImage == null || cooldown <= 0f) return;

        if (skillImage.fillAmount > 0f)
        {
            skillImage.fillAmount -= 1f / cooldown * Time.deltaTime;
            skillImage.fillAmount = Mathf.Clamp01(skillImage.fillAmount);
        }
    }
}
