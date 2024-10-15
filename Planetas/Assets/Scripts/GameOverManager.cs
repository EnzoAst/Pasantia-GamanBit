using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;

    void Start()
    {
        int puntajeFinal = PlayerPrefs.GetInt("PlayerScore", 0);
        ScoreText.text = "Score " + puntajeFinal;

        Debug.Log("Puntaje Final mostrado: " + puntajeFinal);
    }
}
