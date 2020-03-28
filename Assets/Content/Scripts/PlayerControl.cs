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

    private Rigidbody2D trolley;

    private Vector3 movement;

    private void Start()
    {
        trolley = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
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
}
