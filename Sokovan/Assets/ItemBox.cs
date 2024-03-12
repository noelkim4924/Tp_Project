using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    private Renderer myRenderer;
    
    public Color touchColor;
    private Color originalColor;// Start is called before the first frame update
    void Start()
    {   
        // change color of the box
        myRenderer = GetComponent<Renderer>();
        originalColor = myRenderer.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    void OnTriggerEnter(Collider other)
        {
            if(other.tag == "EndPoint")
            {
                myRenderer.material.color = touchColor;
            }
           Debug.Log("EndPoint arrived!");
        }
        // when the player leaves the trigger
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "EndPoint")
        {
            myRenderer.material.color = originalColor;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if(other.tag == "EndPoint")
        {
            myRenderer.material.color = touchColor;
        }
    }
}
