using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;               // Included to modify Unity UI components


public class GameController : MonoBehaviour

{

    public static GameController instance;
    public AudioSource OST;
    

    // Public Text components we set in the Unity Inspector
    public Text timeCounter, countdownText;

    // True if the game is playing, false if it is not
    // Returns false during countdown at beginning 
    // Other classes can read this variable, but it can only be set within this class
    public bool gamePlaying { get; private set; }

    // Number of seconds to count down before starting the game
    public int countdownTime;


    // Time in seconds of when the game began and how long the game has been playing
    private float startTime, elapsedTime;

    // Amount of time the game has been playing. Can be formatted nicely
    TimeSpan timePlaying;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {


        // Initialize the time counter UI
        timeCounter.text = "03:00.00";

        // Game is set to false until countdown completes
        gamePlaying = false;

        // Begins the countdown at the start of the game
        StartCoroutine(CountdownToStart());
    }

    private void BeginGame()
    {
        // Game is now playing and player can move around
        gamePlaying = true;

        // Sets the time the game began playing
        startTime = Time.time;

        PlayOST();
    }

    private void Update()
    {
        // If the game is in a playing state ...
        if (gamePlaying)
        {
            // ... Calculate the time elapsed since the start of the game
            elapsedTime = Time.time - startTime;

            // Generate a TimeSpan from the number of seconds the game has been going on for
            timePlaying = TimeSpan.FromSeconds(elapsedTime);


            // Example format - 01:23.45
            string timePlayingStr = timePlaying.ToString("mm':'ss':'ff");

            // Sets the time counter UI to the formatted string
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