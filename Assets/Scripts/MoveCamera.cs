using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Camera cam;
    public float transitionDuration = 2.5f;
    public Transform targetA;
    public Transform targetB;
    private bool toggle = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            StartCoroutine(Transition());
        }
    }

    IEnumerator Transition()
    {
        float t = 0.0f;
        Vector3 startingPos = cam.transform.position;
        Vector3 finalPosition = targetA.position;

        if (!toggle) {
            finalPosition = targetA.position;
        } else {
            finalPosition = targetB.position;
        }

        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / transitionDuration);
            cam.transform.position = Vector3.Lerp(startingPos, finalPosition, t);
            yield return 0;
        }
        toggle = !toggle;
    }

}
