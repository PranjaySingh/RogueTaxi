using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public string levelToPlay;

    public GameObject mainMenuUI;
    public GameObject instructionsUI;

    private void Awake()
    {
        mainMenuUI.SetActive(true);
        instructionsUI.SetActive(false);
    }

    public void PlayBtn()
    {
        SceneManager.LoadScene(levelToPlay);
    }

    public void InstructionsBtn()
    {
        mainMenuUI.SetActive(false);
        instructionsUI.SetActive(true);

    }

    public void BackBtn()
    {
        instructionsUI.SetActive(false);
        mainMenuUI.SetActive(true);
    }

    public void QuitBtn()
    {
        Application.Quit();
    }
}
