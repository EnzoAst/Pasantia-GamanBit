using UnityEngine;
using TMPro;
using System.Collections;

public class LevelTextDisplay : MonoBehaviour
{
    public TextMeshProUGUI levelText; // Referencia al objeto de texto de TextMeshPro
    public float displayDuration = 1f; // Tiempo que el texto estará visible
    public GameObject panel; // Referencia al objeto de panel

    void Start()
    {
        StartCoroutine(DisplayLevelText());
    }

    private IEnumerator DisplayLevelText()
    {
        levelText.gameObject.SetActive(true); // Activar el texto
        panel.gameObject.SetActive(true); // Activar el panel
        yield return new WaitForSeconds(displayDuration); // Esperar el tiempo de visualización
        levelText.gameObject.SetActive(false); // Desactivar el texto
        panel.gameObject.SetActive(false); // Desactivar el panel
    }
}
