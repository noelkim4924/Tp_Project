using System.Collections;
using System.Collections.Generic;
using TMPro; // TextMesh Pro for UI text elements.
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// NOEL KIM
/// A01259986
/// Manages the general game state, score tracking, and round progression.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Event to signal the resetting of the game state.
    /// </summary>
    public UnityEvent onReset;

    /// <summary>
    /// Static reference to the GameManager instance to allow easy access from other scripts.
    /// </summary>
    public static GameManager instance;

    /// <summary>
    /// UI panel to show when getting ready for a round.
    /// </summary>
    public GameObject readyPanel;

    /// <summary>
    /// Text components for displaying score and messages.
    /// </summary>
    public TMP_Text scoreText, bestScoreText, messageText;

    /// <summary>
    /// State to track if a round of the game is active.
    /// </summary>
    public bool isRoundActive = false;

    /// <summary>
    /// Private variable to keep the current score.
    /// </summary>
    private int score = 0;

    /// <summary>
    /// References to other game components that manage shooting and camera movement.
    /// </summary>
    public ShooterRotator shooterRotator;
    public CamFollow cam;

    /// <summary>
    /// Applies a rainbow color effect to the provided text.
    /// </summary>
    /// <param name="text">The text to apply the effect to.</param>
    /// <returns>A string with each character wrapped in a color tag for rainbow effect.</returns>
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

    void Awake()
    {
        instance = this;
        UpdateUI();
        StartCoroutine("RoundRoutine");
    }

    void Start()
    {
        StartCoroutine("RoundRoutine");
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        UpdateBestScore();
        UpdateUI();
    }

    void UpdateBestScore()
    {
        if (GetBestScore() < score)
        {
            PlayerPrefs.SetInt("BestScore :", score);
        }
    }

    int GetBestScore()
    {
        return PlayerPrefs.GetInt("BestScore");
    }

    void UpdateUI()
    {
        scoreText.text = "Score :" + score;
        bestScoreText.text = "Best Score :" + GetBestScore();
    }

    public void OnBallDestroy()
    {
        UpdateUI();
        isRoundActive = false;
    }

    public void Reset()
    {
        score = 0;
        UpdateUI();
        StartCoroutine(RoundRoutine());
    }

    IEnumerator RoundRoutine()
    {
        onReset.Invoke();
        readyPanel.SetActive(true);
        cam.SetTarget(shooterRotator.transform, CamFollow.State.Idle);
        shooterRotator.enabled = false;
        isRoundActive = false;
        messageText.text = ApplyRainbowColor("READY...");
        yield return new WaitForSeconds(3f);
        isRoundActive = true;
        readyPanel.SetActive(false);
        shooterRotator.enabled = true;
        cam.SetTarget(shooterRotator.transform, CamFollow.State.Ready);
        while (isRoundActive == true)
        {
            yield return null;
        }
        readyPanel.SetActive(true);
        shooterRotator.enabled = false;
        messageText.text = ApplyRainbowColor("Wait for next round...") + "\n\n\nCreated by <color=#FF0000>Noel.Kim</color>";
        yield return new WaitForSeconds(3f);
        Reset();
    }

    void Update()
    {
        // This method is intentionally left empty.
    }
}
