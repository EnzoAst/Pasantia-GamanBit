using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyPlayer : MonoBehaviour
{
    public Transform sun;  // Referencia al objeto Sol
    public float destructionDistance = 1.5f;  // Distancia para la destrucción
    public GameObject explosionEffect;  // Efecto de explosión


    void Update()
    {
        // Verifica la distancia al sol
        if (Vector2.Distance(transform.position, sun.position) < destructionDistance)
        {
            // Instancia el efecto de explosión
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

            Destroy(gameObject);  // Destruye el personaje
            MostrarGameOver();  // Cambia a la escena de Game Over
        }
    }

    void MostrarGameOver()
    {
        // Cargar la escena de Game Over
        SceneManager.LoadScene("GameOverScene");
    }
}
