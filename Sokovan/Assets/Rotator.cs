using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
   
    

    // Update is called once per frame(60 times per second)
    void Update()
    {
        transform.Rotate(60 * Time.deltaTime,60 * Time.deltaTime,60 * Time.deltaTime);
    }
}
