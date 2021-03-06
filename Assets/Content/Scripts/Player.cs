﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    private int playerNumber = 1;

    [SerializeField]
    private float pushForce;

    [SerializeField]
    private float turnForce;

    [SerializeField]
    private float brakeForce;

    [SerializeField]
    private float driftMultiplier = 0.4f;

    [SerializeField]
    private float driftBoost = 15f;

    private float driftTime = 0.0f;

    [SerializeField]
    private AudioClip moveClip;

    [SerializeField]
    private AudioClip crashClip;

    [SerializeField]
    private AudioClip driftClip;

    [SerializeField]
    private Trolley trolley;

    [SerializeField]
    private AudioClip pickUpClip;

    private Rigidbody2D trolleyRb;
    private AudioSource audioSource;

    private bool isDrifting = false;

    private Item closestItem;

    private GameController gameController;

    private void Start()
    {
        trolleyRb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();

        gameController = FindObjectOfType<GameController>();
    }

    private void Update()
    {
        if(!gameController.HasStarted)
        {
            return;
        }

        HandleMovement();
        CheckNearbyItems();
    }

    private void HandleMovement()
    {
        float stopping = 0.1f;
        float vInput = Input.GetAxis("Vertical" + playerNumber.ToString());
        if (vInput >= stopping && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if (vInput < stopping)
        {
            audioSource.Stop();
        }

        if (Input.GetButtonDown("Brake" + playerNumber.ToString()))
        {
            trolley.StartDrifting();
            audioSource.clip = driftClip;
            isDrifting = true;
        }

        if(isDrifting)
        {
            driftTime += Time.deltaTime;
        }

        if (Input.GetButtonUp("Brake" + playerNumber.ToString()))
        {
            trolley.StopDrifting();
            audioSource.clip = moveClip;
            isDrifting = false;
            trolleyRb.AddForce(transform.up * driftBoost * Mathf.Min((driftTime / 3.0f), 1.0f), ForceMode2D.Impulse);
            driftTime = 0.0f;
        }
    }

    private void CheckNearbyItems()
    {

        Collider2D[] foods = Physics2D.OverlapCircleAll(transform.position, 3f, LayerMask.GetMask("Food"));

        if (foods.Length > 0)
        {
            Item closest = foods[0].GetComponent<Item>();

            float bestDist = 999999f;
            foreach (Collider2D food in foods)
            {
                float dist = Vector3.Distance(transform.position, food.transform.position);
                if (dist < bestDist)
                {
                    bestDist = dist;
                    closest = food.GetComponent<Item>();
                }
            }

            if (closest != closestItem && closestItem != null)
            {
                closestItem.StopHighlight();
            }
            closestItem = closest;
            closestItem.Highlight();

            if (Input.GetButtonDown("Interact" + playerNumber.ToString()))
            {
                // Get food in radius of player 'person'
                Destroy(closestItem.GetComponent<Collider2D>());
                trolley.AddItem(closestItem);
                //trolleyRb.mass += 0.01f;

                AudioManager.PlayOnce(pickUpClip);

                closestItem.StopHighlight();
                closestItem = null;
            }
        }
        else
        {
            if (closestItem != null)
            {
                closestItem.StopHighlight();
            }
            closestItem = null;
        }
    }

    private void FixedUpdate()
    {
        if(!gameController.HasStarted)
        {
            return;
        }

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal" + playerNumber.ToString()), Input.GetAxis("Vertical" + playerNumber.ToString()));

        float tempPushForce = movement.y < 0 ? brakeForce : pushForce;

        if(isDrifting)
        {
            tempPushForce *= driftMultiplier;
        }

        trolleyRb.AddForce(transform.up * movement.y * tempPushForce);
        trolleyRb.AddTorque(-movement.x * turnForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioManager.PlayOnce(crashClip);
        Camera.main.GetComponent<CameraShake>().Shake();


        if(collision.gameObject.GetComponent<NPC>() != null)
        {
            NPC npc = collision.gameObject.GetComponent<NPC>();
            npc.Stun();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("FinishLine"))
        {
            gameController.CrossFinishLine();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 3f);
    }

    public Trolley Trolley
    {
        get { return trolley; }
    }
}
