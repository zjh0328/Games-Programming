using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_UI : MonoBehaviour
{
    [SerializeField] private FadeScreen_UI fadeScreen;
    public void ReturnToTitle()
    {
        StartCoroutine(ReturnToTitle_Coroutine());
    }

    private IEnumerator ReturnToTitle_Coroutine()
    {
        GameManager.instance.PauseGame(false);
        LevelManager.instance.ResetLevel();
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("MainMenu");
    }
}
