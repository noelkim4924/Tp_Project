using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject winUI;
    public ItemBox[] itemBoxes;

    public bool isGameOver;
    
    void Start()
    {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    { // GetKeyDown is true only once when the key is pressed
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }
        if(isGameOver  == true){
            return;
        }
        int count = 0;
        for(int i = 0; i < itemBoxes.Length; i++)
        {
            if(itemBoxes[i].isOveraped == true)
            {
                count++;
            }
        }
        if(count == itemBoxes.Length)
        {   
            Debug.Log("Game Over!");
            isGameOver = true;
            winUI.SetActive(true);
        }
    }
}
