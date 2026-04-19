using System.Collections;
using UnityEngine;

public class NPC_Patrol : MonoBehaviour
{
    // GANTI Transform[] MENJADI Vector2[] agar bisa isi angka di Inspector
    public Vector2[] patrolPoints;
    public float speed = 2f;
    public float pauseDuration = 1.5f;

    private bool isPaused;
    private int currentPatrolIndex;
    private Vector2 target;

    private Rigidbody2D rb;
    private Vector3 originalScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;

        if (patrolPoints.Length > 0)
        {
            // Karena sudah Vector2, tidak perlu .position lagi
            target = patrolPoints[currentPatrolIndex];
        }
    }

    void Update()
    {
        if (isPaused)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction = (target - (Vector2)transform.position).normalized;
        rb.linearVelocity = direction * speed;

        // 🔄 Flip Sprite
        if (rb.linearVelocity.x > 0.1f)
        {
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else if (rb.linearVelocity.x < -0.1f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }

        // Cek jarak ke target
        if (Vector2.Distance(transform.position, target) < 0.2f)
        {
            if (!isPaused) StartCoroutine(SetPatrolPoint());
        }
    }

    IEnumerator SetPatrolPoint()
    {
        isPaused = true;
        yield return new WaitForSeconds(pauseDuration);

        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        target = patrolPoints[currentPatrolIndex]; // Tidak pakai .position

        isPaused = false;
    }
}