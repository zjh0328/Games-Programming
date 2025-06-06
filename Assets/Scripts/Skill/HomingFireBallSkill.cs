using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingFireballSkill : Skill
{
    [Header("Homing Fireball Settings")]
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private float fireballSpeed = 7f;
    [SerializeField] private float fireballLifetime = 5f;
    [SerializeField] private float fireballDamage = 30f;

    public override bool AvailableSkill()
    {
        if (cooldownTimer > 0)
        {
            player.fx.CreateText("Skill is in cooldown");
            return false;
        }

        cooldownTimer = cooldown;
        skillLastUseTime = Time.time;

        Transform target = FindClosestEnemy(player.transform);
        if (target == null)
        {
            player.fx.CreateText("No enemies nearby");
            return true; 
        }
        AudioManager.instance.PlaySFX(4, player.transform);
        GameObject fireball = Instantiate(fireballPrefab, player.transform.position, Quaternion.identity);
        var controller = fireball.GetComponent<HomingFireballController>();
        controller.Setup(target, fireballSpeed, fireballDamage, fireballLifetime);

        return true;
    }



}
