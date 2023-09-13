using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartIceFloorTile : MonoBehaviour
{
    //public Transform firstChild;

    //GameObject ChildGameObject2 = ParentGameObject.transform.GetChild(1).gameObject;

    void Update()
    {
        checkPlayer();
    }

    /*
        public void meltStartingTile()
        {
            firstChild = this.gameObject.transform.GetChild(0);
            Debug.Log("This is the first child " + firstChild);
        }
    */
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
