using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LaserCollision : MonoBehaviour
{
    public event Action onGhostDestroyed;
    public GameObject hitEffect;

    //public static int destroyedGhost = 0;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            return;

        if (other.CompareTag("Enemy")){
            HitGhost(other);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void HitGhost(Collider ghost) {

        onGhostDestroyed?.Invoke();

        Instantiate(hitEffect,gameObject.transform.position, Quaternion.identity);
        
        Destroy(gameObject);
        Destroy(ghost.gameObject);
    }


}
