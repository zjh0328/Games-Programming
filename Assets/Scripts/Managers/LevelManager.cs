using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public int EnemyLevel { get; private set; } = 1;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetEnemyLevel(int level)
    {
        EnemyLevel = Mathf.Clamp(level, 1, 3);
    }

    public int GetEnemyLevel()
    {
        return EnemyLevel;
    }

    public void ResetLevel()
    {
        EnemyLevel = 1; 
    }
}
