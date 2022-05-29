using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject Panel1;
    public GameObject Panel2;
    public AudioSource MenuSFX;
    public AudioSource Button_sfx;

    public void NewGame()
    {
        PlayButton();
        SceneManager.LoadScene("Level_1");
    }
    public void Exit()
    {
        PlayButton();
        Application.Quit();
    }

    public void Options()
    {
        PlayButton();
        Panel1.SetActive(false);
        Panel2.SetActive(true);
    }

    public void Volver()
    {
        PlayButton();
        Panel1.SetActive(true);
        Panel2.SetActive(false);
    }
    
    public void PlayMenu()
    {
        MenuSFX.Play ();
    }

    public void PlayButton()
    {
        Button_sfx.Play ();
    }
}
