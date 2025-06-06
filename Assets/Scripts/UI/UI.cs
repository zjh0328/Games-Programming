using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public static UI instance;

    [Header("UI Elements")]
    [SerializeField] private GameObject Pause_UI;
    [SerializeField] private GameObject InGame_UI;

    [Header("Try again")]
    public FadeScreen_UI fadeScreen;
    [SerializeField] private GameObject diedText;
    [SerializeField] private GameObject tryAgainButton;

    [Header("Thank you Page")]
    [SerializeField] private GameObject thankYouText;
    [SerializeField] private GameObject returnToTitleButton;

    private bool uiKeyFunctioning = true;
    private GameObject currentUI;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;
    }

    private void Start()
    {
        SwitchToMenu(InGame_UI);
        fadeScreen.gameObject.SetActive(true);
        uiKeyFunctioning = true;
    }

    private void Update()
    {
        if (uiKeyFunctioning && Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentUI != InGame_UI)
                SwitchToMenu(InGame_UI);
            else
                SwitchToMenu(Pause_UI);
        }
    }

    public void ReturnToGame()
    {
        SwitchToMenu(InGame_UI); 
    }

    public void SwitchToMenu(GameObject menu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            bool isFadeScreen = (transform.GetChild(i).GetComponent<FadeScreen_UI>() != null);
            if (!isFadeScreen)
            {
                transform.GetChild(i).gameObject.SetActive(false);
                currentUI = null;
            }
        }

        if (menu != null)
        {
            menu.SetActive(true);
            currentUI = menu;
        }

        if (menu == InGame_UI)
            GameManager.instance?.PauseGame(false);
        else
            GameManager.instance?.PauseGame(true);
    }

    public void SwitchToThankYouText(string achievedEndingText)
    {
        uiKeyFunctioning = false;
        fadeScreen.FadeOut();
        StartCoroutine(ThankYouTextCoroutine(achievedEndingText));
    }

    private IEnumerator ThankYouTextCoroutine(string achievedEndingText)
    {
        yield return new WaitForSeconds(1.5f);
        thankYouText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        returnToTitleButton.SetActive(true);
    }

    public void ReturnToMainMenu()
    {
        StartCoroutine(ReturnToMainMenuCoroutine());
    }

    private IEnumerator ReturnToMainMenuCoroutine()
    {
        GameManager.instance.PauseGame(false);
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("MainMenu");
    }

    public void SwitchToTryAgain()
    {
        uiKeyFunctioning = false;
        fadeScreen.FadeOut();
        StartCoroutine(TryAgainCoroutine());
    }

    private IEnumerator TryAgainCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        diedText.SetActive(true);
        yield return new WaitForSeconds(1f);
        tryAgainButton.SetActive(true);
    }

    public void RestartGame()
    {
        GameManager.instance.RestartGame();
    }
}
