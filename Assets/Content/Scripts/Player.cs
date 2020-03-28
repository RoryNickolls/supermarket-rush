using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private float pushForce;

    [SerializeField]
    private float turnForce;

    [SerializeField]
    private float brakeForce;

    [SerializeField]
    private AudioClip crashClip;

    [SerializeField]
    private Trolley trolley;

    private Rigidbody2D trolleyRb;
    private AudioSource audioSource;

    private bool isDrifting = false;

    private void Start()
    {
        trolleyRb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float stopping = 0.1f;
        float speed = trolleyRb.velocity.magnitude;
        if(Input.GetAxis("Vertical") >= stopping && !audioSource.isPlaying)
        {
            audioSource.Play();
        } 
        else if(Input.GetAxis("Vertical") < stopping) 
        {
            audioSource.Stop();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            trolley.StartDrifting();
            isDrifting = true;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            trolley.StopDrifting();
            isDrifting = false;
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        float tempPushForce = movement.y < 0 ? brakeForce : pushForce;

        if(isDrifting)
        {
            tempPushForce *= 0.5f;
        }

        trolleyRb.AddForce(transform.up * movement.y * tempPushForce);
        trolleyRb.AddTorque(-movement.x * turnForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioManager.PlayOnce(crashClip);
    }
}
