using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public DashSkill Dash { get; private set; }
    public BlackholeSkill Blackhole { get; private set; }
    public HomingFireballSkill Fireball { get; private set; }


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

        Dash = GetComponent<DashSkill>();
        Blackhole = GetComponent<BlackholeSkill>();
        Fireball = GetComponent<HomingFireballSkill>();
    }
}
