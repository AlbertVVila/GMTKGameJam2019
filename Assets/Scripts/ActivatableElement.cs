using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableElement : MonoBehaviour
{
    public bool toggeable = false;
    public bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "tongue")
        {

            if (toggeable)
            {
                Debug.Log("Toggled: " + this.name);
                if (triggered)
                {
                    // second status
                } else {
                    // first status
                }

                triggered = !triggered;
            }

            if (!triggered)
            {
                // only one behavior
                Debug.Log("Triggered: " + this.name);
                triggered = true;
            } 

        }
    }

}
