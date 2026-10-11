using UnityEngine;

public class EnemyMGController : MonoBehaviour
{
    [SerializeField] private float movementDistance = 5;
    [SerializeField] private float speed = 5;
    [SerializeField] private float damage = 10;

    private Rigidbody2D rb;
    private bool movingLeft;
    private float leftEdge, rightEdge;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        leftEdge  = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
    }

    void FixedUpdate()
    {
        if (movementDistance <= 0f) return; // враг стоит на месте

        float dir = movingLeft ? -1f : 1f;
        Vector2 pos = rb.position;
        pos.x = Mathf.Clamp(pos.x + dir * speed * Time.fixedDeltaTime, leftEdge, rightEdge);
        rb.MovePosition(pos);

        if (pos.x <= leftEdge) movingLeft = false;
        else if (pos.x >= rightEdge) movingLeft = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        var health = collision.GetComponent<HealthSystem>();
        if (health != null)
            health.TakeDamage(damage);
    }
}