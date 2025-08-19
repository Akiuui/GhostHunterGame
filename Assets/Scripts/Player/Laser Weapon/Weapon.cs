using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    public event Action<float> OnChargeChanged;

    public GameObject projectile;
    public Transform tipOfGun;
    public HUDController hudController;

    private GameManager managerScript;


    public float laserSpeed = 10f;

    private bool isCharging = false;    
    public float charge = 0f;
    public float chargeMax = 2f;

    private void Start()
    {
       managerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public float Charge
    {
        get { return charge; }
        private set
        {
            if (charge != value)
            {
                charge = value;
                OnChargeChanged?.Invoke(charge);
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isCharging = true;
        }
        if (Input.GetMouseButtonUp(0) && isCharging == true)
        {
            isCharging = false;
        }

        if (isCharging)
        {
            if (Charge < chargeMax)
            {
                Charge += Time.deltaTime;

            }
        }

        if (!isCharging)
        {
            if (Charge >= chargeMax)
            {
                FireCharged();
            }
            Charge = 0;
        }

    }

    private void FireCharged() {
  
        Quaternion offsetRotation = Quaternion.Euler(0, 90, 0);
        GameObject laser = Instantiate(projectile, tipOfGun.position, transform.rotation * offsetRotation);
        laser.GetComponent<Rigidbody>().linearVelocity = -laser.transform.right * laserSpeed;

        LaserCollision laserCollision = laser.GetComponent<LaserCollision>();

        laserCollision.onGhostDestroyed += managerScript.ghostFired;
        laserCollision.onGhostDestroyed += hudController.UpdateGhostCounterUI;

        Destroy(laser, 3);

    }
}
