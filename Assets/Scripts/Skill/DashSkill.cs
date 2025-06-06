using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashSkill : Skill
{
    protected override void Start()
    {
        base.Start();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        AudioManager.instance.PlaySFX(0, player.transform);
    }
}
