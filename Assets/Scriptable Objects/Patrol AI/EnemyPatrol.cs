using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed = 360f; // koselere gelince sadece acik odaya nasil baktiririm bilmiyorum oyuzden 360
    [SerializeField] float waitTime; // etrafi kolacan etme suresi

    private int currentPointIndex = 0;
    private bool isWaiting = false;

    private enum State
    {
        Patrolling,
        Waiting
    }

    private State currentState;

    void Start()
    {
        if (patrolPoints.Length > 0)
        {
            currentState = State.Patrolling;
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Patrolling:
                Patrol();
                break;
            case State.Waiting:
                // yeni state gecisi
                break;
        }
    }

    private void Patrol()
    {
        if (patrolPoints.Length == 0 || isWaiting)
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
            currentState = State.Waiting;
            StartCoroutine(WaitAndRotate());
        }
    }

    private IEnumerator WaitAndRotate()
    {
        isWaiting = true;

        float elapsedTime = 0f;
        while (elapsedTime < waitTime)
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        isWaiting = false;
        currentState = State.Patrolling;
    }
}
