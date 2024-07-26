using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Transform playerTransform;

    [Header("Camera Settings")]
    [SerializeField] Vector3 positionOffset = Vector3.zero;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }


    void LateUpdate()
    {
        transform.position = playerTransform.position + positionOffset;
    }
}
