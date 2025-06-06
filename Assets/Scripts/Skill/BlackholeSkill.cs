using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlackholeSkill : Skill
{
    [Header("Blackhole Settings")]
    [SerializeField] private GameObject blackholeEffectPrefab;
    [SerializeField] private float radius;
    [SerializeField] private int damage;

    protected override void Start()
    {
        base.Start();
    }

    public override void UseSkill()
{
    base.UseSkill();

    if (!SkillReady()) return;

    if (blackholeEffectPrefab)
    {
        GameObject go = Instantiate(blackholeEffectPrefab, player.transform.position, Quaternion.identity);

        BlackholeSkillController controller = go.GetComponent<BlackholeSkillController>();
        if (controller != null)
        {
            controller.Setup(damage, radius);
        }
    }

    AudioManager.instance.PlaySFX(8, player.transform);

    cooldownTimer = cooldown;
}


    public override bool AvailableSkill()
    {
        return base.AvailableSkill(); 
    }
}
