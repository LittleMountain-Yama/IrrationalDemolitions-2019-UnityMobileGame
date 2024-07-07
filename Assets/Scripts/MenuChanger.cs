using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuChanger : MonoBehaviour
{
    public GameObject credits;
    public GameObject buttons;

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenCredits()
    {
        credits.SetActive(true);
        buttons.SetActive(false);
    }

    public void CloseCredits()
    {
        credits.SetActive(false);
        buttons.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
