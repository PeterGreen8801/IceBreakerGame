using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + "This is the name of the object that entered the trigger");
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("The NAME of the object that left the trigger is " + other.gameObject.name);

        //Set Starting Ice Floor child not active
        Debug.Log(transform.GetChild(0));
        transform.GetChild(0).gameObject.SetActive(false);

        //Set Water Floor child active
        Debug.Log(transform.GetChild(1));
        transform.GetChild(1).gameObject.SetActive(true);
    }
}
