using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComestibleInsect : MonoBehaviour
{
    public bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "tongue")
        {
            if (!triggered)
            {
                // only one behavior
                Debug.Log("Triggered: " + this.name);
                triggered = true;
            } 

        }

        if (collision.name == "Player")
        {
            if (!triggered)
            {
                Destroy(this.gameObject);
            }
        }

    }

}
