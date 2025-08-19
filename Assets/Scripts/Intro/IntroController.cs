using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    public CameraMovement cameraMovementScript;
    public GameObject[] elementsToDestroy;
    public GameObject[] elementsToActivate;
    private void Start()
    {
        cameraMovementScript.Begin();
        float animationDuration = cameraMovementScript.getTransitionDuration();
        StartCoroutine(WaitAndRun(animationDuration, AfterAnimation));
    }

    private void AfterAnimation()
    {
        //print("Gotova animacija");

        foreach (GameObject element in elementsToDestroy)
        {
            Destroy(element);
        }
        foreach (GameObject element in elementsToActivate)
        {
            element.SetActive(true);
        }

    }
    IEnumerator WaitAndRun(float waitTime, Action runFunc)
    {
        yield return new WaitForSeconds(waitTime);

        runFunc();

    }
}
