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
                GameObject.Find("Player").GetComponent<Player>().tongueRange = GameObject.Find("Player").GetComponent<Player>().tongueRange + 2.0f; 
                amount.text = (int.Parse(amount.text) + 1).ToString();
                if (int.Parse(amount.text) == 5)
                {
                    // game finished
                    wonTitle.enabled = true;
                }
                Destroy(this.gameObject, 1.5f);
            }
        }

    }



}
