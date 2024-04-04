using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Noel Kim
/// A01259986
/// Manages the overall game state, including win conditions and scene management.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Reference to the UI element that is displayed when the player wins.
    /// </summary>
    public GameObject winUI;

    /// <summary>
    /// Array of all item boxes in the scene.
    /// </summary>
    public ItemBox[] itemBoxes;

    /// <summary>
    /// Flag to determine if the game is over.
    /// </summary>
    public bool isGameOver;
    
    /// <summary>
    /// Called before the first frame update to initialize the game as not being over.
    /// </summary>
    void Start()
    {
        isGameOver = false;
    }

    /// <summary>
    /// Called once per frame to handle input, check game state, and manage win conditions.
    /// </summary>
    void Update()
    {
        // If the player presses the Space key to reload the current scene, effectively restarting the game.
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }

        // Exit out of the method if the game is already over to prevent further checks.
        if(isGameOver == true)
        {
            return;
        }

        // Counter for how many item boxes have been overlapped.
        int count = 0;

        // Iterate over all item boxes to check their state.
        for(int i = 0; i < itemBoxes.Length; i++)
        {
            if(itemBoxes[i].isOveraped == true)
            {
                count++;
            }
        }

        // If all item boxes are overlapped, log a message to the console, set the game to be over, and display the win UI.
        if(count == itemBoxes.Length)
        {
            Debug.Log("Game Over!");
            isGameOver = true;
            winUI.SetActive(true);
        }
    }

    /// <summary>
    /// Checks if the game should end, can be called to check the state outside of the Update loop.
    /// </summary>
    public void CheckGameEnd()
    {
        int count = 0;

        foreach(var itemBox in itemBoxes)
        {
            if(itemBox.isOveraped)
                count++;
        }

        if(count == itemBoxes.Length)
        {
            Debug.Log("Game Over!");
            isGameOver = true;
            if (winUI != null) winUI.SetActive(true);
        }
    }
}
