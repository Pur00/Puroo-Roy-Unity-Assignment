using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // To use the Text Mesh Pro

public class statistics : MonoBehaviour
{
    // In this script, we are managing the UI things. This includes the number of cubes left, the time left and the game over screen.
    float timeLeft = 120f; // 120 seconds = 2 minutes
    public int numberOfCubes = 5; // How many cubes there will be in each level
    public GameObject gameOverText, youWonText; // The game over and you won text
    public TextMeshProUGUI timerText, cubeText; // The time left and the cubes left
    public Transform playerPosition; // To check if the player is going out of bounds
    public float minX, minY, minZ, maxX, maxY, maxZ; // These will be used to decide what is considered out of bounds

    // Update is called once per frame
    void Update()
    {
        // The timer going down
            timeLeft -= Time.deltaTime;

            // Calculate minutes and seconds
            int minutes = Mathf.FloorToInt (timeLeft / 60);
            int seconds = Mathf.FloorToInt (timeLeft % 60);

            // Display game over when the time ends
            if (timeLeft < 0.5f)
            {
                Time.timeScale = 0f;
                gameOverText.SetActive (true);
            }

            // Display game over when the player goes out of bounds
            
            if (playerPosition.position.x < minX || playerPosition.position.x > maxX
            || playerPosition.position.y < minY || playerPosition.position.y > maxY
            ||playerPosition.position.z < minZ || playerPosition.position.z > maxZ)
            {
                Time.timeScale = 0f;
                gameOverText.SetActive (true);
            }
            // Display the timer in the format "minute:seconds" instead of just seconds
            timerText.text = string.Format ("{0:00}:{1:00}", minutes, seconds);
            cubeText.text = string.Format ("Cubes left: " + numberOfCubes);
            if (numberOfCubes == 0)
            {
                youWonText.SetActive (true);
                // The timer, number of cubes left and the game over text get removed when the player collects all the cubes
                Destroy (gameOverText);
                timerText.text = string.Format ("");
                cubeText.text = string.Format ("");
            }
    }
}
