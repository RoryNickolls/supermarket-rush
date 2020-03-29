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
    private float driftMultiplier = 0.4f;

    [SerializeField]
    private float driftBoost = 15f;

    [SerializeField]
    private AudioClip crashClip;

    [SerializeField]
    private Trolley trolley;

    [SerializeField]
    private AudioClip pickUpClip;

    private Rigidbody2D trolleyRb;
    private AudioSource audioSource;

    private bool isDrifting = false;

    private Item closestItem;

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

            trolleyRb.AddForce(transform.up * driftBoost, ForceMode2D.Impulse);
        }

        Collider2D[] foods = Physics2D.OverlapCircleAll(transform.position, 2f, LayerMask.GetMask("Food"));

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

            if(closest != closestItem && closestItem != null)
            {
                closestItem.StopHighlight();
            }
            closestItem = closest;
            closestItem.Highlight();

            if (Input.GetKeyDown(KeyCode.E))
            {
                // Get food in radius of player 'person'
                Destroy(closestItem.GetComponent<Collider2D>());
                trolley.AddItem(closestItem.gameObject);
                trolleyRb.mass += 0.05f;

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
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

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
        Camera.main.GetComponent<CameraShake>().IsShaking = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 2f);
    }
}
