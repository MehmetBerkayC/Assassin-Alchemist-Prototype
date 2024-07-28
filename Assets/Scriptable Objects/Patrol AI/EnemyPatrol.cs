using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int currentPointIndex = 0;
    public float speed;

    void Update()
    {
        if (patrolPoints.Length == 0)
            return;

        Transform targetPoint = patrolPoints[currentPointIndex];
        Vector2 direction = targetPoint.position - transform.position;
        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (direction != Vector2.zero)
        {
            transform.right = direction;
        }

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.5f)
        {
            currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        }
    }
}

