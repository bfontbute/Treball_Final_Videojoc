using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject Panel1;
    public GameObject Panel2;


    public void NewGame()
    {
        SceneManager.LoadScene("Level_1");
    }
    public void Exit()
    {
        Application.Quit();
    }

    public void Options()
    {
        Panel1.SetActive(false);
        Panel2.SetActive(true);
    }

    public void Volver()
    {
        Panel1.SetActive(true);
        Panel2.SetActive(false);
    }
}
