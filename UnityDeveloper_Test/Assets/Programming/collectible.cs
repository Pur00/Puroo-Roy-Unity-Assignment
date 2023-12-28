using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectible : MonoBehaviour
{
    // If the player touches it, it will disappear and the total score will increase
    public statistics statsScript; // This is to access the statistics.cs script, so that we can make the number of cubes left reduce

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag ("Player")) // If will disappear only when the player touches it, and not by the wall touching it
        {
            Destroy(gameObject);
            statsScript.numberOfCubes--;
        }
    }
}
