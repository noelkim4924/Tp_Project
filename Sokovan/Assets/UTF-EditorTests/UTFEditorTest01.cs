using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class UTFEditorTest01
{
public class PlayerTests
{}}
//      [Test]
//     public void PlayerMovesForwardWhenUpArrowPressed()
//     {
//         // Arrange
//         GameObject playerGameObject = new GameObject("Player");
//         Player player = playerGameObject.AddComponent<Player>();
//         Rigidbody playerRigidbody = playerGameObject.AddComponent<Rigidbody>();

//         // Set initial position for simulation
//         Vector3 initialPosition = new Vector3(0, 0, 0);
//         playerRigidbody.position = initialPosition;

//         // Act
//         // Simulate pressing the up arrow key
//         player.SimulateInput(Vector3.forward);
//         // Simulate movement for one frame by calling MovePosition
//         playerRigidbody.MovePosition(playerRigidbody.position + player.MovementSpeed * Vector3.forward * Time.fixedDeltaTime);

//         // Assert
//         // Verify the player has moved from the original position
//         Assert.AreNotEqual(initialPosition, playerRigidbody.position, "The player should move forward when the up arrow key is pressed.");
//     }

//     [UnityTest]
//     public IEnumerator PlayerDoesNotMoveWhenNoInput()
//     {
//         // Arrange
//         GameObject playerGameObject = new GameObject("Player");
//         Player player = playerGameObject.AddComponent<Player>();
//         Rigidbody playerRigidbody = playerGameObject.AddComponent<Rigidbody>();

//         // Set initial position
//         Vector3 initialPosition = new Vector3(0, 0, 0);
//         playerRigidbody.position = initialPosition;

//         // Act
//         // Do nothing as no input is given
//         // Wait for Time.fixedDeltaTime to verify the player doesn't move
//         yield return new WaitForSeconds(Time.fixedDeltaTime);

//         // Assert
//         // Verify the player remains at the original position
//         Assert.AreEqual(initialPosition, playerRigidbody.position, "The player should not move when there is no input.");
//     }

//     [Test]
//     public void GameEndsWhenAllItemBoxesOverlapped()
//     {
//         // Arrange
//         GameObject gameManagerGameObject = new GameObject("GameManager"); // Create a new GameObject named 'GameManager'.
//         GameManager gameManager = gameManagerGameObject.AddComponent<GameManager>(); // Add a 'GameManager' component.
//         GameObject itemBox1GameObject = new GameObject("ItemBox1"); // Create a new GameObject named 'ItemBox1'.
//         ItemBox itemBox1 = itemBox1GameObject.AddComponent<ItemBox>(); // Add an 'ItemBox' component.
//         GameObject itemBox2GameObject = new GameObject("ItemBox2"); // Create a new GameObject named 'ItemBox2'.
//         ItemBox itemBox2 = itemBox2GameObject.AddComponent<ItemBox>(); // Add an 'ItemBox' component.
//         gameManager.itemBoxes = new ItemBox[] { itemBox1, itemBox2 }; // Assign the 'itemBoxes' array to 'GameManager'.

//         // Act
//         itemBox1.isOveraped = true; // Assume 'ItemBox1' is overlapped.
//         itemBox2.isOveraped = true; // Assume 'ItemBox2' is overlapped.
//         gameManager.CheckGameEnd(); // Call the method to check the game end condition.

//         // Assert
//         Assert.IsTrue(gameManager.isGameOver, "The game should end when all item boxes are overlapped with the end points.");
//     }

//     [UnityTest]
//     public IEnumerator GameManagerEndsGameWithEnumeratorPasses()
//     {
//         // This is similar to the above test but uses UnityTest for coroutine-based testing if necessary.
//         yield return null;
//     }

//     [Test]
//     public void GameNotOverAtStart()
//     {
//         // Arrange
//         GameObject gameManagerGameObject = new GameObject("GameManager");
//         GameManager gameManager = gameManagerGameObject.AddComponent<GameManager>();

//         // Act
//         // Test just the initialization of the game manager

//         // Assert
//         Assert.IsFalse(gameManager.isGameOver, "The game should not be in an over state at the start.");
//     }

//     [Test]
//     public void GameNotEndsWhenNoItemBoxesOverlapped()
//     {
//         // Arrange
//         GameObject gameManagerGameObject = new GameObject("GameManager");
//         GameManager gameManager = gameManagerGameObject.AddComponent<GameManager>();
//         GameObject itemBox1GameObject = new GameObject("ItemBox1");
//         ItemBox itemBox1 = itemBox1GameObject.AddComponent<ItemBox>();
//         GameObject itemBox2GameObject = new GameObject("ItemBox2");
//         ItemBox itemBox2 = itemBox2GameObject.AddComponent<ItemBox>();
//         gameManager.itemBoxes = new ItemBox[] { itemBox1, itemBox2 };

//         // Act
//         itemBox1.isOveraped = false; // Assume 'ItemBox1' is not overlapped.
//         itemBox2.isOveraped = false; // Assume 'ItemBox2' is also not overlapped.
//         gameManager.CheckGameEnd();

//         // Assert
//         Assert.IsFalse(gameManager.isGameOver, "The game should not end if none of the item boxes are overlapped with the end points.");
//     }

//     [UnityTest]
//     public IEnumerator GameContinuesWhenSpaceIsNotPressed()
//     {
//         // Arrange
//         GameObject gameManagerGameObject = new GameObject("GameManager");
//         GameManager gameManager = gameManagerGameObject.AddComponent<GameManager>();
        
//         // Act
//         // Do nothing as the space key is not pressed
        
//         // Assert
//         yield return null; // Wait for one frame
//         Assert.IsFalse(gameManager.isGameOver, "The game should continue if the space key is not pressed.");
//     }
// }