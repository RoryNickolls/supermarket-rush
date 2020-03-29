using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private float defaultAmplitude = 0.25f;

    [SerializeField]
    private float defaultDuration = 0.1f;

    private float amplitude;
    private float duration;

    private float shakeTimer = 0.0f;

    private bool isShaking = false;
    private Vector3 original;

    private void Start()
    {
        original = transform.position;
    }

    private void Update()
    {
        if(shakeTimer <= duration && isShaking)
        {
            transform.position = original + (Vector3)Random.insideUnitCircle * amplitude;
            shakeTimer += Time.deltaTime;
        }
        else
        {
            transform.position = original;
            isShaking = false;
        }
    }
    
    public void Shake(float length, float amplitude)
    {
        shakeTimer = 0.0f;
        this.amplitude = amplitude;
        this.duration = length;
        isShaking = true;
    }

    public void Shake()
    {
        shakeTimer = 0.0f;
        duration = defaultDuration;
        amplitude = defaultAmplitude;
        isShaking = true;
    }
}
