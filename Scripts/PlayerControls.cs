using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup")]
    [Tooltip("Vertical speed of the ship")] [SerializeField] float thrustSpeed = 10f;
    [Tooltip("Horizontal player movement range")] [SerializeField] float xRange = 10f;
    [Tooltip("Vertical player movement range")] [SerializeField] float yRange = 7f;

    [Header("Laser array")]
    [Tooltip("Add player lasers here")] [SerializeField] GameObject[] lasers;

    [Header("Screen poisition tuning")]
    [SerializeField] float pitchFactor = -2f;
    [SerializeField] float yawFactor = 2f;

    [Header("Player input tuning")]
    [SerializeField] float rollControl = -20f;
    [SerializeField] float pitchControl = -20f;
    float xThrow, yThrow;
    
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * thrustSpeed;
        float newX = transform.localPosition.x + xOffset;
        float clampedX = Mathf.Clamp(newX, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * thrustSpeed;
        float newY = transform.localPosition.y + yOffset;
        float clampedY = Mathf.Clamp(newY, -yRange, yRange);

        transform.localPosition = new Vector3(clampedX, clampedY, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        float pitch = transform.localPosition.y * pitchFactor + yThrow * pitchControl;
        float yaw = transform.localPosition.x * yawFactor;
        float roll = xThrow * rollControl;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring()
    {
        if(Input.GetButton("Fire1"))
        {
            ActivateLasers(true);    
        }
        else
        {
            ActivateLasers(false); 
        }
    }

    void ActivateLasers(bool check)
    {
        foreach(GameObject laser in lasers)
        {
            var emission = laser.GetComponent<ParticleSystem>().emission;
            emission.enabled = check;
        }        
    }
}
