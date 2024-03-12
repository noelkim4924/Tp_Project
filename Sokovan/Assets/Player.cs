using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  public GameManager gameManager;

   public float speed = 10f;
   private Rigidbody playerRigidbody; 
   // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
      if(gameManager.isGameOver == true)
      {
        return;
      }
  // user input
    float inputX = Input.GetAxis("Horizontal");
  // "Horizontal" is a built-in keyword in Unity
  // it is a string that represents the left and right arrow keys
  // it is a float that represents the value of the left and right arrow keys
    // it is a value between -1 and 1(the reason why joisticks work)
    float inputZ = Input.GetAxis("Vertical");

    float fallSpeed = playerRigidbody.velocity.y;

   //playerRigidbody.AddForce(inputX * speed, 0, inputZ * speed); (관성 inertia exists )
   // but velocity is better than force because it ignore inertia x
   Vector3 velocity = new Vector3(inputX,0,inputZ);
   
   velocity = velocity * speed;

    velocity.y = fallSpeed;

   playerRigidbody.velocity = velocity;





//   if(Input.GetKey(KeyCode.W))
//   {
//         playerRigidbody.AddForce(0,0,speed);
//   }
//   if(Input.GetKey(KeyCode.A))
//   {
//         playerRigidbody.AddForce(-speed,0,0);
//   }
//   if(Input.GetKey(KeyCode.S))
//   {
//         playerRigidbody.AddForce(0,0,-speed);
//   }
//   if(Input.GetKey(KeyCode.D))
//   {
//         playerRigidbody.AddForce(speed,0,0);
//   }


    }
}
