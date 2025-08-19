using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    public Camera currentCamera;
    public Transform cameraTarget;
    public float cameraTransitionDuration = 2.0f;

    public float getTransitionDuration() => cameraTransitionDuration;

    public void Begin() {
        StartCoroutine(FlyCameraToTarget());
    }

    IEnumerator FlyCameraToTarget() {

        if (currentCamera == null || cameraTarget == null)
        {
            Debug.LogError("Camera or Player Target not assigned in MenuManagerScript!");

            yield break;
        }


        Vector3 startPosition = currentCamera.transform.position;
        Quaternion startRotation = currentCamera.transform.rotation;

        float elapsedTime = 0f;

        while (elapsedTime < cameraTransitionDuration)
        {
            currentCamera.transform.position = Vector3.Lerp(startPosition, cameraTarget.position, elapsedTime / cameraTransitionDuration);
            currentCamera.transform.rotation = Quaternion.Slerp(startRotation, cameraTarget.rotation, elapsedTime / cameraTransitionDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        currentCamera.transform.position = cameraTarget.position;
        currentCamera.transform.rotation = cameraTarget.rotation;

    }
}
