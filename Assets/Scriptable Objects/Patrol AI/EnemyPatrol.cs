using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float speed;
    public float rotationSpeed = 360f; // Derece/saniye
    public float detectionTime = 3f;   // Oyuncuyu tespit etmek için gereken süre
    public PolygonCollider2D detectionCollider; // Tespit alanı için kullanılan collider
    public GameObject player; // Oyuncu karakteri

    private int currentPointIndex = 0;
    private bool isWaiting = false;
    private bool isPlayerDetected = false;
    private float detectionCounter = 0f;
    private PatrolNode currentNode;

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
                // Coroutine tarafından yönetilir, burada bir şey yapmaya gerek yok
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
            // Node'a ulaşıldığında bekleme durumuna geç
            PatrolNode node = targetPoint.GetComponent<PatrolNode>();
            if (node != null)
            {
                currentNode = node;
                currentState = State.Waiting;
                StartCoroutine(WaitAndLookAtAngles(node));
            }
        }
    }

    private IEnumerator WaitAndLookAtAngles(PatrolNode node)
    {
        isWaiting = true;

        if (node.rotate360)
        {
            // Eğer 360 derece dönecekse
            float elapsedTime = 0f;
            while (elapsedTime < node.waitTime)
            {
                transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        else
        {
            // Eğer belirli bir açı aralığında dönecekse
            float elapsedTime = 0f;
            float normalizedMinAngle = NormalizeAngle(node.minAngle);
            float normalizedMaxAngle = NormalizeAngle(node.maxAngle);
            bool isLookingRight = node.prioritizeClockwise;

            while (elapsedTime < node.waitTime)
            {
                float targetAngle = isLookingRight ? normalizedMaxAngle : normalizedMinAngle;
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, targetAngle));
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
                {
                    // Yönü değiştir
                    isLookingRight = !isLookingRight;
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }

        // Bekleme süresi dolduğunda devriyeye devam et
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        isWaiting = false;
        currentState = State.Patrolling;
    }


    private float NormalizeAngle(float angle)
    {
        // Açıyı 0-360 derece aralığına getir
        angle = angle % 360;
        if (angle < 0)
            angle += 360;
        return angle;
    }


    private void DetectPlayer()
    {
        if (isPlayerDetected)
        {
            detectionCounter += Time.deltaTime;

            // Hareketi durdur ve oyuncuya bak
            Vector2 direction = (player.transform.position - transform.position).normalized;
            transform.right = direction;

            if (detectionCounter >= detectionTime)
            {
                // Oyuncu tespit edildi, oyun sonu mantığını burada uygula
                Debug.Log("Player detected! Game Over.");
                // Örnek: SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
