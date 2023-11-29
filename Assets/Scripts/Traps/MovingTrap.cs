using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrap : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public float speed = 4.0f;
    public int damage = 1;

    private float journeyLength;
    private float startTime;
    private bool movingForward = true;

    void Start()
    {
        journeyLength = Vector3.Distance(startPoint.position, endPoint.position);
        startTime = Time.time;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
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