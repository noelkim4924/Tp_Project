using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// This class manages the overall game state, including win conditions and scene management.
public class GameManager : MonoBehaviour
{
    public GameObject winUI; // Reference to the UI element that is displayed when the player wins.
    public ItemBox[] itemBoxes; // Array of all item boxes in the scene.

    public bool isGameOver; // Flag to determine if the game is over.
    
    // Start is called before the first frame update.
    void Start()
    {
        // Initialize the game state as not being over.
        isGameOver = false;
    }

    // Update is called once per frame.
    void Update()
    {
        // If the player presses the Space key...
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // Reload the current scene, effectively restarting the game.
            SceneManager.LoadScene(0);
        }

        // If the game is already over, exit out of the Update method to prevent further checks.
        if(isGameOver == true){
            return;
        }

        // Counter for how many item boxes have been overlapped.
        int count = 0;

        // Iterate over all item boxes to check their state.
        for(int i = 0; i < itemBoxes.Length; i++)
        {
            // If the current item box is overlapped, increment the counter.
            if(itemBoxes[i].isOveraped == true)
            {
                count++;
            }
        }

        // If all item boxes are overlapped...
        if(count == itemBoxes.Length)
        {   
            // Log a message to the console for debugging.
            Debug.Log("Game Over!");

            // Set the game to be over and display the win UI.
            isGameOver = true;
            winUI.SetActive(true);
        }
    }

    // Method to check if the game should end, can be called to check the state outside of the Update loop.
    public void CheckGameEnd()
    {
        // Reset the counter.
        int count = 0;

        // Use a foreach loop to iterate over all item boxes.
        foreach(var itemBox in itemBoxes)
        {
            // If the item box is overlapped, increment the counter.
            if(itemBox.isOveraped)
                count++;
        }

        // If all item boxes are overlapped...
        if(count == itemBoxes.Length)
        {
            // Log a message to the console.
            Debug.Log("Game Over!");

            // Set the game to be over and, if the win UI reference is not null, activate the win UI.
            isGameOver = true;
            if (winUI != null) winUI.SetActive(true);
        }
    }
}
