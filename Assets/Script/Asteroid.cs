using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerHealth playerHealth =  other.GetComponent<PlayerHealth>();

        if(playerHealth == null)
        {
            return;
        }
        playerHealth.Crash();
    }

    private void OnBecameInvisible()
    { //destroy aestroid when they go out of the screen otherwise they'll just keep spaawning and never destroyed and will drop fram rate and use more memory
        Destroy(gameObject);
    }

}
