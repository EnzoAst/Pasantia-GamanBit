using UnityEngine;

public class SpaceShipProjectile : MonoBehaviour
{
    public float speed = 1f;
    public float lifetime = Mathf.Infinity; // Hacer que la vida �til sea infinita
    private float currentLifetime = 0f;
    public GameObject effect; // Asigna el prefab del efecto en el Inspector

    void Update()
    {
        // Mover el proyectil hacia abajo en l�nea recta
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // Contar el tiempo de vida del proyectil
        currentLifetime += Time.deltaTime;
        if (currentLifetime > lifetime)
        {
            Destroy(gameObject); // Destruir el proyectil inmediatamente
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Llama a la funci�n que reduce la vida del jugador
            PlayerController.Instance.ReduceLife();

            // Instancia el efecto visual
            if (effect != null)
            {
                GameObject efecto = Instantiate(effect, collision.transform.position, Quaternion.identity);
                Destroy(efecto, 1f); // Destruir el efecto despu�s de 1 segundo
            }

            // Reubica al jugador
            collision.transform.position = PlayerController.Instance.GetInitialPosition(); // Aseg�rate de que esto funcione
            Destroy(gameObject); // Destruir el proyectil despu�s de que se use
        }
    }
}
