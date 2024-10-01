using UnityEngine;

public class DestroyPlayer : MonoBehaviour
{
    public Transform sun;           // Referencia al objeto Sol
    public float destructionDistance = 1.5f;  // Distancia para la destrucci�n
    public GameObject explosionEffect;   // Prefab del efecto de explosi�n

    void Update()
    {
        // Verifica la distancia al sol
        if (Vector2.Distance(transform.position, sun.position) < destructionDistance)
        {
            // Instancia el efecto de explosi�n
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject); // Destruye el personaje
        }
    }
}
