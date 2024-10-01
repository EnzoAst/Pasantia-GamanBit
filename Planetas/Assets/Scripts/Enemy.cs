using UnityEngine;
using UnityEngine.SceneManagement; // Para poder volver a cargar la escena

public class Enemy : MonoBehaviour
{
    public GameObject explosionVFX; // Prefab del efecto visual

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Verifica si el jugador colisiona con el enemigo
        {


            Destroy(collision.transform.gameObject);

            Debug.Log("¡El jugador ha muerto!"); // Mensaje de depuración

            // Instanciar el efecto VFX en la posición del enemigo
            Instantiate(explosionVFX, transform.position, Quaternion.identity);

            // Llama al método ReloadScene después de 2 segundos
            Invoke("ReloadScene", 1f); // Cambia 2f a 1f si prefieres 1 segundo
        }
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarga la escena actual
    }
}
