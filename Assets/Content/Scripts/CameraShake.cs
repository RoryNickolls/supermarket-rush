using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private float shakeAmplitude = 0.25f;

    [SerializeField]
    private float shakeDuration = 0.1f;
    private float shakeTimer = 0.0f;

    private bool isShaking = false;
    private Vector3 original;

    private void Start()
    {
        original = transform.position;
    }

    private void Update()
    {
        if(shakeTimer <= shakeDuration && isShaking)
        {
            transform.position = original + (Vector3)Random.insideUnitCircle * shakeAmplitude;
            shakeTimer += Time.deltaTime;
        }
        else
        {
            transform.position = original;
            isShaking = false;
        }
    }

    public bool IsShaking
    {
        get { return isShaking; }
        set { 
            isShaking = value;
            if(isShaking)
            {
                shakeTimer = 0.0f;
            }
        }
    }
}
