using UnityEngine;

public class PatrolNode : MonoBehaviour
{
    public float minAngle = -45f; // Minimum açı
    public float maxAngle = 45f;  // Maksimum açı
    public float waitTime = 2f;   // Bu node'da bekleme süresi
    public float detectionRadius = 2f; // Dairenin yarıçapı
    public bool prioritizeClockwise = true; // Hangi yönden başlamalı
    public bool rotate360 = false; // 360 derece dönme durumu

    private void OnDrawGizmos()
    {
        // Node'un pozisyonunu temsil eden bir küre çiz
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.2f);

        if (rotate360)
        {
            // Dairenin sınırlarını çiz
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
        else
        {
            // Açıları normalize etme
            float normalizedMinAngle = NormalizeAngle(minAngle);
            float normalizedMaxAngle = NormalizeAngle(maxAngle);

            // Açı aralığını temsil eden çizgiler çiz
            Gizmos.color = Color.blue;
            Vector3 minDirection = Quaternion.Euler(0, 0, normalizedMinAngle) * Vector3.right;
            Vector3 maxDirection = Quaternion.Euler(0, 0, normalizedMaxAngle) * Vector3.right;
            Gizmos.DrawLine(transform.position, transform.position + minDirection * detectionRadius);
            Gizmos.DrawLine(transform.position, transform.position + maxDirection * detectionRadius);
        }
    }

    private float NormalizeAngle(float angle)
    {
        // Açıyı 0-360 derece aralığına getir
        angle = angle % 360;
        if (angle < 0)
            angle += 360;
        return angle;
    }
}
