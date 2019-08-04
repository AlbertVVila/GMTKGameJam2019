using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ComestibleInsect : MonoBehaviour
{
    public bool triggered = false;
    public Text amount;
    public Text wonTitle;

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
                collision.GetComponent<Player>().tongueRange =+ 2.0f;
                amount.text = (int.Parse(amount.text) + 1).ToString();
                if (int.Parse(amount.text) == 5)
                {
                    // game finished
                    wonTitle.enabled = true;
                }
                Destroy(this.gameObject);
            }
        }

    }

}
