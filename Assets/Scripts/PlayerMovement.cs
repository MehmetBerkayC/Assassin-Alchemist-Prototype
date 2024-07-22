using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement & Settings")]
    [SerializeField] private float speed = 10;
    [SerializeField] private float interactRadius = 2;

    [SerializeField] private LayerMask interactableMask;
    //[SerializeField] private float maxSpeed = 20;

    private Vector2 _inputs = Vector2.zero;

    [Header("References")]
    [SerializeField] private Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetInputs();

        if (Input.GetKeyDown(KeyCode.E)) {
            Collider2D col = Physics2D.OverlapCircle(transform.position, interactRadius, interactableMask);
            Debug.Log(col);
            if (col != null && col.gameObject.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
            }
        }
    }

    private void FixedUpdate()
    {
        body.velocity = _inputs * speed;
    }

    private void GetInputs()
    {
        _inputs.x = Input.GetAxisRaw("Horizontal");
        _inputs.y = Input.GetAxisRaw("Vertical");
        _inputs.Normalize();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
