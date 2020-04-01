using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Transform[] patrolPath;

    private int currentIndex = 0;

    [SerializeField]
    private float stunTime = 0.3f;

    private bool stunned = false;
    private float stunTimer = 0.0f;


    private void Start()
    {
        // Extract positions from path
        transform.position = patrolPath[0].position;
    }

    private void Update()
    {
        if(stunned)
        {
            stunTimer += Time.deltaTime;
            if(stunTimer >= stunTime)
            {
                stunned = false;
            }
            return;
        }

        int nextIndex = (currentIndex + 1) % patrolPath.Length;

        Vector3 dir = (patrolPath[nextIndex].position - transform.position).normalized;
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90);
        GetComponent<Rigidbody2D>().velocity = dir * moveSpeed;

        if(Vector3.Distance(transform.position, patrolPath[nextIndex].position) <= 0.1f)
        {
            currentIndex = nextIndex;
        }
    }

    public void Stun()
    {
        stunned = true;
        stunTimer = 0.0f;
    }
}
