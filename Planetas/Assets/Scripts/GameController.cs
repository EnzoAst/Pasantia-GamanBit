using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public int puntos = 0; // Puntos del jugador
    public int vidas = 3; // Vidas iniciales

    private const string LIVES_KEY = "PlayerLives";
    private const string SCORE_KEY = "PlayerScore";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Cargar las vidas y los puntos
        if (PlayerPrefs.HasKey(LIVES_KEY))
        {
            vidas = PlayerPrefs.GetInt(LIVES_KEY);

            // Si las vidas guardadas son 0, reiniciar a 3
            if (vidas == 0)
            {
                vidas = 3; // Reiniciar a 3 si el jugador perdió todas las vidas
            }
        }
        else
        {
            vidas = 3; // Si es la primera vez, empieza con 3 vidas
        }

        if (PlayerPrefs.HasKey(SCORE_KEY))
        {
            puntos = PlayerPrefs.GetInt(SCORE_KEY);
        }
        else
        {
            puntos = 0; // Si no hay puntaje guardado, empezar con 0
        }
    }

    public void RestarVida()
    {
        vidas--;

        PlayerPrefs.SetInt(LIVES_KEY, vidas);
        PlayerPrefs.Save();

        if (vidas <= 0)
        {
            PlayerController.Instance.MostrarGameOver(); // Mostrar la escena de Game Over
        }
    }

    // Método para sumar puntos
    public void SumarPuntos(int cantidad)
    {
        puntos += cantidad; // Sumar la cantidad de puntos
        PlayerPrefs.SetInt(SCORE_KEY, puntos); // Guardar el puntaje cada vez que se suma
        PlayerPrefs.Save(); // Asegurarse de que los cambios se guarden
        Debug.Log("Puntos actuales: " + puntos);
    }

}
