using UnityEngine;

public class Collectible : MonoBehaviour
{
    public float rotationSpeed = 50f; // Velocidad de rotaci�n
    private bool fueRecogido = false; // Marca si la estrella ha sido recogida

    void Start()
    {
        // Verificar si la estrella ha sido recogida previamente
        if (GameManager.Instance.IsEstrellaRecogida(transform.position))
        {
            Destroy(gameObject); // Destruir la estrella si ya fue recogida
        }
    }


    void Update()
    {
        // Rota la estrella continuamente en el eje Z (ya que estamos en 2D)
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colisi�n con: " + other.gameObject.name); // Log de colisi�n

        // Verifica si el objeto que colisiona es el jugador y si no ha sido recogido a�n
        if (other.CompareTag("Player") && !fueRecogido)
        {
            // Sumar puntos al jugador
            GameController.Instance.SumarPuntos(100);


            Debug.Log("Collectible recogido. Puntos: " + GameController.Instance.puntos); // Log para confirmar la recogida

            // A�adir la posici�n de la estrella al GameManager
            GameManager.Instance.AddEstrella(transform.position);

            // Destruir la estrella
            Destroy(gameObject);

            // Marca el collectible como recogido
            fueRecogido = true;
        }
    }
}
