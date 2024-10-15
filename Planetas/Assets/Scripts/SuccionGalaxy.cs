using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SuccionGalaxy : MonoBehaviour
{
    public Transform galaxyOPortal;           // Referencia al objeto Galaxy
    public float succionDistance = 1.5f;  // Distancia para la succión
    public GameObject explosionEffect;   // Prefab del efecto de explosión
    public string nextLevel = "Nivel2";  // Nombre de la escena del siguiente nivel

    void Update()
    {
        // Verifica la distancia a la galaxia
        if (Vector2.Distance(transform.position, galaxyOPortal.position) < succionDistance)
        {
            // Instancia el efecto de explosión
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

            // Simula que el personaje es succionado y transfiere al siguiente nivel
            StartCoroutine(LoadNextSceneWithDelay(0.3f)); // Cambia a la escena del siguiente nivel después de 2 segundos
        }
    }

    private IEnumerator LoadNextSceneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextLevel); // Carga la escena del siguiente nivel
    }
}