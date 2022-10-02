using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject gameWinUI;
    public GameObject gameLoseUI;
    public GameObject LevelStartUI;
    public string levelToLoad;

    private void Awake()
    {
        LevelStartUI.SetActive(true);
        gameWinUI.SetActive(false);
        gameLoseUI.SetActive(false);
        Time.timeScale = 0;
    }

    private void Update()
    {
        if(GameObject.FindGameObjectWithTag("DriftingMat") == null)
        {
            Invoke("CallWinScreen", 0.5f);
        }
        else if(GameObject.FindGameObjectWithTag("Player") == null)
        {
            Invoke("CallLostScreen", 1f);
        }
    }

    public void CallWinScreen()
    {
        gameWinUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void CallLostScreen()
    {
        gameLoseUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void NextLvlBtn()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void RestartBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MenuBtn()
    {
        SceneManager.LoadScene("Menu");
    }

    public void StartGameBtn()
    {
        LevelStartUI.SetActive(false);
        Time.timeScale = 1;
    }


}
