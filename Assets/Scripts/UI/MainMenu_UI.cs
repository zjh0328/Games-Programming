using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu_UI : MonoBehaviour
{
    public static MainMenu_UI instance;

    [SerializeField] private string sceneName = "MainScene";
    [SerializeField] private FadeScreen_UI fadeScreen;

    [Header("Exit Confirm")]
    [SerializeField] private GameObject exitConfirmWindow;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        Time.timeScale = 1f;
    }

    public void StartEasyGame() => StartGameWithDifficulty(1);
    public void StartNormalGame() => StartGameWithDifficulty(2);
    public void StartHardGame() => StartGameWithDifficulty(3);

    private void StartGameWithDifficulty(int level)
    {
        LevelManager.instance.SetEnemyLevel(level);

        Debug.Log("Current Difficulty: " + level);

        StartCoroutine(LoadSceneWithFade(1.5f, sceneName));
    }

    public void Exit()
    {
        Debug.Log("Game exited");
        Application.Quit();
    }

    private IEnumerator LoadSceneWithFade(float delayTime, string targetScene)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(targetScene);
    }

    public void ShowExitConfirmWindow()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        exitConfirmWindow.SetActive(true);
    }
}
