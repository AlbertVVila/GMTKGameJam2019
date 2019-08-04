using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Camera cam;
    public float transitionDuration = 2.5f;
    public Transform target;

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
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / transitionDuration);
            cam.transform.position = Vector3.Lerp(startingPos, target.position, t);
            yield return 0;
        }
    }

}
