using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IceFloorTile : MonoBehaviour
{

    [SerializeField] private bool interactedWith = false;

    public void Interact()
    {
        Debug.Log("Interact!");
        interactedWith = true;

    }

    public void meltTile()
    {
        //Will change the IceFloorTile to a WaterTile
    }
}
