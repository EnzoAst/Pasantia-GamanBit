using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    public float airJumpForceMultiplier = 0.5f; // Multiplicador para reducir el salto en el aire
    private Rigidbody2D rb;
    private bool enPlaneta = false;
    private Transform currentPlanet;

    public float planetRadiusOffset = 0.5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Desactivar la gravedad estándar
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if (enPlaneta && currentPlanet != null)
        {
            Vector3 directionToPlanet = (transform.position - currentPlanet.position).normalized;
            transform.position = currentPlanet.position + directionToPlanet * (currentPlanet.localScale.x / 2 + planetRadiusOffset);

            Vector3 gravityDirection = (currentPlanet.position - transform.position).normalized;
            float angle = Mathf.Atan2(gravityDirection.y, gravityDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + 90);
        }
    }

    void Jump()
    {
        enPlaneta = false;
        transform.SetParent(null);

        Vector2 jumpDirection = Vector2.zero;
        if (Input.GetKey(KeyCode.A))
            jumpDirection = Vector2.left;
        if (Input.GetKey(KeyCode.D))
            jumpDirection = Vector2.right;
        if (Input.GetKey(KeyCode.W))
            jumpDirection = Vector2.up;
        if (Input.GetKey(KeyCode.S))
            jumpDirection = Vector2.down;

        if (jumpDirection == Vector2.zero)
            jumpDirection = Vector2.up;

        rb.velocity = Vector2.zero;

        // Modificar la fuerza del salto dependiendo si está en el planeta o en el aire
        float appliedJumpForce = enPlaneta ? jumpForce : jumpForce * airJumpForceMultiplier;
        rb.AddForce(jumpDirection * appliedJumpForce, ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Planet"))
        {
            enPlaneta = true;
            rb.velocity = Vector2.zero;
            currentPlanet = collision.transform;

            transform.SetParent(currentPlanet);
        }
    }
}
