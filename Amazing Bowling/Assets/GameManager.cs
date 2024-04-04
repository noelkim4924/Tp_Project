using System;
using System.Collections;
using System.Collections.Generic;
using TMPro; // TextMesh Pro for UI text elements.
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// This class manages the general game state, score tracking, and round progression.
public class GameManager : MonoBehaviour
{
    // Event to signal resetting of the game state.
    public UnityEvent onReset;

    // Static reference to the GameManager instance to allow easy access from other scripts.
    public static GameManager instance;

    // UI panel to show when getting ready for a round.
    public GameObject readyPanel;
    
    private string ApplyRainbowColor(string text)
{
    string[] colors = new string[] { "#FF0000", "#FF7F00", "#FFFF00", "#00FF00", "#0000FF", "#4B0082", "#9400D3" };
    string rainbowText = "";
    for (int i = 0; i < text.Length; i++)
    {
        string color = colors[i % colors.Length];
        rainbowText += $"<color={color}>{text[i]}</color>";
    }
    return rainbowText;
}
    // Text components for displaying score and messages.
    public TMP_Text scoreText;
    public TMP_Text bestScoreText;
    public TMP_Text messageText;

    // State to track if a round of the game is active.
    public bool isRoundActive = false;

    // Private variable to keep the current score.
    private int score = 0;

    // References to other game components that manage shooting and camera movement.
    public ShooterRotator shooterRotator;
    public CamFollow cam;

    // Awake is called when the script instance is being loaded.
    void Awake()
    {
        // Assign the static instance to this instance of GameManager.
        instance = this;

        // Update the UI elements with the current game state.
        UpdateUI();

        // Start the coroutine to manage the game rounds.
        StartCoroutine("RoundRoutine");
    }

    // Start is called before the first frame update.
    void Start()
    {
        // Optionally, another call to start the round routine if not already started in Awake.
        StartCoroutine("RoundRoutine");
    }

    // Method to increase the score when called, typically from other game objects.
    public void AddScore(int newScore)
    {
        score += newScore; // Increment the score.
        UpdateBestScore(); // Check if the best score needs updating.
        UpdateUI(); // Update the UI with the new score.
    }

    // Method to update the best score if the current score exceeds it.
    void UpdateBestScore()
    {
        if(GetBestScore() < score)
        {
            // Set the new best score in PlayerPrefs, a storage system for saving game data.
            PlayerPrefs.SetInt("BestScore :", +score);
        }
    }

    // Method to retrieve the best score from PlayerPrefs.
    int GetBestScore()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore");
        return bestScore;
    }  
    
    // Method to update the UI text elements with the current and best scores.
    void UpdateUI()
    {
        scoreText.text = "Score :" + score;
        bestScoreText.text = "Best Score :" + GetBestScore();
    }

    // Called when the ball is destroyed to update UI and end the round.
    public void OnBallDestroy()
    {
        UpdateUI(); // Update the score UI.
        isRoundActive = false; // Mark the round as no longer active.
    }

    // Reset the game to its initial state and start a new round.
    public void Reset()
    {
        score = 0; // Reset the current score to 0.
        UpdateUI(); // Update the UI to reflect the reset.

        // Restart the round routine coroutine.
        StartCoroutine(RoundRoutine());
    }

    // Coroutine to handle the sequence of a single round.
    IEnumerator RoundRoutine()
    {
        // Invoke the reset event which can reset game objects and states.
        onReset.Invoke();

        // Show the ready panel UI and set up the camera.
        readyPanel.SetActive(true);
        cam.SetTarget(shooterRotator.transform, CamFollow.State.Idle);
        shooterRotator.enabled = false;

        isRoundActive = false;

        messageText.text = "<color=#FFA550>READY...";

        // Wait for a moment before starting the round.
        yield return new WaitForSeconds(3f);

        // Actual gameplay starts, enabling the shooter and hiding the ready panel.
        isRoundActive = true;
        readyPanel.SetActive(false);
        shooterRotator.enabled = true;

        cam.SetTarget(shooterRotator.transform, CamFollow.State.Ready);

        // Wait as long as the round is active.
        while(isRoundActive == true)
        {
            yield return null;
        }

        // When the round ends, prepare for the next round.
        readyPanel.SetActive(true);
        shooterRotator.enabled = false;

        messageText.text = "<color=#FFA500>Wait for next round...</color>\n\n\nCreat by <color=#FF0000>Noel.Kim</color>";

        // Wait for a moment before resetting for a new round.
        yield return new WaitForSeconds(3f);
        Reset();
    }

    // The Update method is called once per frame. Currently, it's empty but can be used for updates that check game state.
    void Update()
    {

    }
}
