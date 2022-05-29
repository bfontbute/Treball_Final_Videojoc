using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;               // Included to modify Unity UI components


public class GameController : MonoBehaviour

{
    [SerializeField]
    private static GameController instance;
    [SerializeField]
    private AudioSource OST;
    [SerializeField]
    private Text timeCounter, countdownText;
    public bool gamePlaying { get; private set; }
    [SerializeField]
    private int countdownTime;
    private float startTime, elapsedTime;
    TimeSpan timePlaying;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timeCounter.text = "03:00.00";
        gamePlaying = false;
        StartCoroutine(CountdownToStart());
    }

    private void BeginGame()
    {
        gamePlaying = true;
        startTime = Time.time;
        PlayOST();
    }

    private void Update()
    {
        if (gamePlaying)
        {
            elapsedTime = Time.time - startTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = timePlaying.ToString("mm':'ss':'ff");
            timeCounter.text = timePlayingStr;
        }
    }

  
    IEnumerator CountdownToStart()
    {
        
        while (countdownTime > 0)
        {

            countdownText.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            
            countdownTime--;
        }

        
        BeginGame();

        
        countdownText.text = "GO!";

        
        yield return new WaitForSeconds(1f);

        
        countdownText.gameObject.SetActive(false);
    }

    public void PlayOST()
    {
        OST.Play ();
    }
}