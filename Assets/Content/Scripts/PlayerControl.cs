using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    [SerializeField]
    private float pushForce;

    [SerializeField]
    private float turnForce;

    [SerializeField]
    private float brakeForce;

    [SerializeField]
    private AudioClip crashClip;

    private Rigidbody2D trolley;
    private AudioSource audioSource;

    private void Start()
    {
        trolley = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float stopping = 0.1f;
        float speed = trolley.velocity.magnitude;
        if(Input.GetAxis("Vertical") >= stopping && !audioSource.isPlaying)
        {
            audioSource.Play();
  
        } 
        else if(Input.GetAxis("Vertical") < stopping) 
        {
            audioSource.Stop();
        }
    }

    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        float tempPushForce = movement.y < 0 ? brakeForce : pushForce;
        if(Input.GetKeyDown(KeyCode.Space))
        {
            tempPushForce *= 0.5f;
        } 
        trolley.AddForce(transform.up * movement.y * tempPushForce);
        trolley.AddTorque(-movement.x * turnForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioManager.PlayOnce(crashClip);
    }
}
