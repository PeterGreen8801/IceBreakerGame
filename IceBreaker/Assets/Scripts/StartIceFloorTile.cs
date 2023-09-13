using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartIceFloorTile : MonoBehaviour
{
    void Update()
    {
        checkPlayer();
    }

    void checkPlayer()
    {
        /*
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                Debug.Log("The player is in the collider");
                break;
            }
            else
            {
                Debug.Log("Player is NOT in the collider");
            }
        }
        */

    }

}
