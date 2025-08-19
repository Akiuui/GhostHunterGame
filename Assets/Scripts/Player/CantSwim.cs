using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CantSwim : MonoBehaviour
{
    public Transform waterHeight;
    public Transform spawnPoint;
    
    private float timer;
    public float countdown = 3f;

    private bool isRespawning = false;
    public float respawnCooldown = 1f;

    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        timer = countdown;
    }
    void Update()
    {
        if (isRespawning)
            return;

        if (waterHeight.position.y > transform.position.y)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                StartCoroutine(RespawnPlayer());
            }
        }
    }
    private IEnumerator RespawnPlayer() {
        print("Respawn started");
        isRespawning = true;

        rb.MovePosition(spawnPoint.position);

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        timer = countdown;

        yield return new WaitForSeconds(respawnCooldown);

        isRespawning = false;
        print("Position Restarted");


    }
}

