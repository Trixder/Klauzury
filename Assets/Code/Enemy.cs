using UnityEngine;

public class Enemy : MonoBehaviour {
    public static Transform player;
    public float detectionDistance = 6;
    public float speed = 0.75f;
    public float maxSpeed = 0.75f;
    public float avoidanceForceMultiplier = 5f;
    public float raySpacing = 1.25f;
    public float wonder = 5f;
    public Vector2 destination;
    private LayerMask obstacleLayerMask;
    public Transform unstuck;
    private bool chase = false;

    private Rigidbody2D rb;

    void Start() {
        player = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        obstacleLayerMask = LayerMask.GetMask("Solid");
        Wonder();
    }

    private void Update() {
        if (Time.timeScale == 0) return;

        if (speed != maxSpeed) {
            speed += Time.deltaTime;
            if (speed > maxSpeed) speed = maxSpeed;
        }

        if (Vector2.Distance(player.position, transform.position) > detectionDistance) {
            if (chase | Vector2.Distance(destination, (Vector2)transform.position) < 1f) Wonder();
        } else Player();

        Vector2 targetDirection = ((Vector3)destination - transform.position).normalized;

        Vector2 targetPos = destination;
        Vector2 thisPos = transform.position;
        targetPos.x = targetPos.x - thisPos.x;
        targetPos.y = targetPos.y - thisPos.y;
        float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        //TODO 5 change how dodging obstacles work
        RaycastHit2D[] hits = new RaycastHit2D[3];
        Vector2 rayStart = new Vector2(transform.position.x, transform.position.y) + targetDirection * rb.velocity.magnitude * Time.deltaTime;
        for (int i = 0; i < 3; i++) {
            Vector2 rayDirection = Quaternion.AngleAxis((i - 1) * 30f, Vector3.forward) * targetDirection;
            RaycastHit2D raycastHit2D = Physics2D.Raycast(rayStart, rayDirection, raySpacing, obstacleLayerMask);
            hits[i] = raycastHit2D;
            Debug.DrawRay(rayStart, rayDirection * raySpacing, Color.red);
        }

        foreach (RaycastHit2D hit in hits) if (hit.collider != null) {
                if (!chase) Wonder();
                rb.position = Vector2.Lerp(rb.position, unstuck.position, Time.deltaTime / speed);
        }

        rb.position = Vector2.Lerp(rb.position, destination, Time.deltaTime / (Vector2.Distance(destination, transform.position) / (speed * 3.25f)));
    }

    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.transform.tag == "Player" & speed == maxSpeed) {
            speed = 0;
            PlayerHealth.ChangeHP(-20f);
        }
    }

    private void Player() {
        destination = player.position;
        if (chase) return;
        chase = true;
        detectionDistance = 6;
        maxSpeed = 0.75f;
        speed = 0.75f;
        raySpacing = 1.25f;
    }

    private void Wonder() {
        destination = transform.position + new Vector3(Random.Range(-wonder, wonder), Random.Range(-wonder, wonder), 0);
        if (!chase) return;
        chase = false;
        detectionDistance = 4;
        maxSpeed = 0.25f;
        speed = 0.25f;
        raySpacing = 1;
    }
}