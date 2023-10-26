using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrap : MonoBehaviour
{
    public Transform startPoint; 
    public Transform endPoint;   
    public float speed = 3.0f;
    public int damage = 1;

    private float journeyLength;
    private float startTime;
    private bool movingForward = true;

    void Start()
    {
        journeyLength = Vector3.Distance(startPoint.position, endPoint.position);
        startTime = Time.time;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null && !playerHealth.isInvulnerable)
            {
                playerHealth.TakeDamage(damage);

                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        float distanceCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distanceCovered / journeyLength;

        if (movingForward)
        {
            transform.position = Vector3.Lerp(startPoint.position, endPoint.position, fractionOfJourney);
        }
        else
        {
            transform.position = Vector3.Lerp(endPoint.position, startPoint.position, fractionOfJourney);
        }

        if (fractionOfJourney >= 1.0f)
        {
            movingForward = !movingForward;
            startTime = Time.time;
        }
    }
}
