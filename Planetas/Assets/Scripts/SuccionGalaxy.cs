using UnityEngine;

public class SuccionGalaxy : MonoBehaviour
{
    public Transform galaxy;           // Referencia al objeto Galaxy
    public float succionDistance = 1.5f;  // Distancia para la succi�n
    public GameObject explosionEffect;   // Prefab del efecto de explosi�n

    void Update()
    {
        // Verifica la distancia a la galaxia
        if (Vector2.Distance(transform.position, galaxy.position) < succionDistance)
        {
            // Instancia el efecto de explosi�n
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject); // Destruye el personaje como si entrara succionado a la galaxia
        }
    }
}
