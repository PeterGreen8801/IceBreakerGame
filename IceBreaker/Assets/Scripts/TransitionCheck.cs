using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionCheck : MonoBehaviour
{
    public float numberOfTimesSteppedOn = 0;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + "This is the name of the object that entered the trigger");
        numberOfTimesSteppedOn++;
        Debug.Log("Number of steps is now: " + numberOfTimesSteppedOn);
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("The NAME of the object that left the trigger is " + other.gameObject.name);

        if (numberOfTimesSteppedOn == 1)
        {
            //Set Starting Ice Floor child not active
            Debug.Log(transform.GetChild(0));
            transform.GetChild(0).gameObject.SetActive(false);

            //Set Water Floor child active
            Debug.Log(transform.GetChild(1));
            transform.GetChild(1).gameObject.SetActive(true);
        }

        if (numberOfTimesSteppedOn > 1)
        {
            //Set Starting Ice Floor child not active
            Debug.Log(transform.GetChild(1));
            transform.GetChild(1).gameObject.SetActive(false);

            //Set Water Floor child active
            Debug.Log(transform.GetChild(2));
            transform.GetChild(2).gameObject.SetActive(true);
        }

    }
}
