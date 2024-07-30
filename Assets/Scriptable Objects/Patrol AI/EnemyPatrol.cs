//using System.Collections;
//using UnityEngine;

//public class EnemyPatrol : MonoBehaviour
//{
//    [SerializeField] Transform[] patrolPoints;
//    [SerializeField] float speed;
//    [SerializeField] float rotationSpeed = 360f; // koselere gelince sadece acik odaya nasil baktiririm bilmiyorum oyuzden 360
//    [SerializeField] float waitTime; // etrafi kolacan etme suresi

//    private int currentPointIndex = 0;
//    private bool isWaiting = false;

//    private enum State
//    {
//        Patrolling,
//        Waiting
//    }

//    private State currentState;

//    void Start()
//    {
//        if (patrolPoints.Length > 0)
//        {
//            currentState = State.Patrolling;
//        }
//    }

//    void Update()
//    {
//        switch (currentState)
//        {
//            case State.Patrolling:
//                Patrol();
//                break;
//            case State.Waiting:
//                // yeni state gecisi
//                break;
//        }
//    }

//    private void Patrol()
//    {
//        if (patrolPoints.Length == 0 || isWaiting)
//            return;

//        Transform targetPoint = patrolPoints[currentPointIndex];
//        Vector2 direction = targetPoint.position - transform.position;
//        transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

//        if (direction != Vector2.zero)
//        {
//            transform.right = direction;
//        }

//        if (Vector2.Distance(transform.position, targetPoint.position) < 0.5f)
//        {
//            currentState = State.Waiting;
//            StartCoroutine(WaitAndRotate());
//        }
//    }

//    private IEnumerator WaitAndRotate()
//    {
//        isWaiting = true;

//        float elapsedTime = 0f;
//        while (elapsedTime < waitTime)
//        {
//            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
//            elapsedTime += Time.deltaTime;
//            yield return null;
//        }

//        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
//        isWaiting = false;
//        currentState = State.Patrolling;
//    }
//}

using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed;
    public float rotationSpeed = 360f; // Degrees per second
    public float waitTime = 2f; // Time to wait and rotate at each point
    public float detectionTime = 3f; // Time required to detect the player
    public PolygonCollider2D detectionCollider; // The collider used for detection
    public GameObject player; // The player character

    private int currentPointIndex = 0;
    private bool isWaiting = false;
    private bool isPlayerDetected = false;
    private float detectionCounter = 0f;

    private enum State
    {
        Patrolling,
        Waiting,
        Detecting
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
                // Waiting handled by coroutine, no need to do anything here
                break;
            case State.Detecting:
                DetectPlayer();
                break;
        }
    }

    private void Patrol()
    {
        if (patrolPoints.Length == 0 || isWaiting || isPlayerDetected)
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

    private void DetectPlayer()
    {
        if (isPlayerDetected)
        {
            detectionCounter += Time.deltaTime;

            // Stop moving and face the player
            Vector2 direction = (player.transform.position - transform.position).normalized;
            transform.right = direction;

            if (detectionCounter >= detectionTime)
            {
                // Player detected, game over logic here
                Debug.Log("Player detected! Game Over.");
                // Implement game over logic
                // Example: SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            isPlayerDetected = true;
            detectionCounter = 0f;
            currentState = State.Detecting;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            isPlayerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            isPlayerDetected = false;
            detectionCounter = 0f;
            currentState = State.Patrolling;
        }
    }
}
