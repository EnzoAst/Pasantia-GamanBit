using TMPro;
using UnityEngine;

public class GameCongratulationsManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText; // Asignar desde el inspector el UI para el texto de puntaje

    void Start()
    {
        // Obtener el puntaje final desde PlayerPrefs
        int puntajeFinal = PlayerPrefs.GetInt("PlayerScore", 0);

        // Mostrar el puntaje en el UI
        ScoreText.text = "Score " + puntajeFinal;

        Debug.Log("Puntaje Final mostrado en Congratulations: " + puntajeFinal);
    }
}
