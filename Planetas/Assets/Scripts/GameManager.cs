using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton para acceder fácilmente desde otros scripts
    public HashSet<Vector3> estrellasRecogidas = new HashSet<Vector3>(); // Almacena posiciones de estrellas recogidas

    private void Awake()
    {
        // Implementación del patrón Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Mantiene este objeto al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Destruye el objeto duplicado
        }
    }

    public void AddEstrella(Vector3 position)
    {
        estrellasRecogidas.Add(position); // Agregar la posición de la estrella recogida
    }

    public bool IsEstrellaRecogida(Vector3 position)
    {
        return estrellasRecogidas.Contains(position); // Verificar si la estrella ya ha sido recogida
    }
}