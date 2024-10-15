using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // Para TextMesh Pro
using UnityEngine.UI;
using System.Collections; // Para Image

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance; // Singleton para acceder fácilmente

    public float jumpForce = 10f; // Fuerza de salto
    public float airJumpForceMultiplier = 0.5f; // Multiplicador de fuerza de salto en el aire
    private Rigidbody2D rb; // Referencia al Rigidbody2D del jugador
    private bool enPlaneta = false; // Indica si el jugador está en un planeta
    private Transform currentPlanet; // El planeta actual en el que está el jugador

    public float planetRadiusOffset = 0.5f; // Offset para el radio del planeta

    private Vector3 posicionInicial; // Posición inicial del jugador

    public TextMeshProUGUI puntosTexto; // Cambia a TextMeshProUGUI para el puntaje
    public Image[] corazones; // Arreglo de imágenes para los corazones (UI)

    private Animator animator; // Referencia al Animator

    private const string SCORE_KEY = "PlayerScore"; // Clave para guardar el puntaje

    private void Awake()
    {
        Instance = this;
    }

    public void ReduceLife()
    {
        // Acceder a las vidas a través del GameController
        if (GameController.Instance.vidas > 0)
        {
            GameController.Instance.RestarVida(); // Restar vida en el GameController

            // Aquí podrías agregar un Debug.Log para verificar que se resta la vida
            Debug.Log("Vidas restantes: " + GameController.Instance.vidas);

            // Verificar si el jugador se queda sin vidas
            if (GameController.Instance.vidas <= 0)
            {
                // Si no quedan vidas, mostrar la pantalla de Game Over
                MostrarGameOver();
            }
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Obtener el Rigidbody2D
        rb.gravityScale = 0; // Desactivar la gravedad estándar
        posicionInicial = transform.position; // Guardar la posición inicial del jugador
        animator = GetComponent<Animator>(); // Obtener el Animator

        // Cargar el puntaje desde PlayerPrefs
        if (PlayerPrefs.HasKey(SCORE_KEY))
        {
            GameController.Instance.puntos = PlayerPrefs.GetInt(SCORE_KEY);
        }
        else
        {
            GameController.Instance.puntos = 0; // Si no hay un puntaje guardado, empieza en 0
        }

        // Actualizar la UI al inicio
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump(); // Llamar a la función de salto
        }

        if (enPlaneta && currentPlanet != null)
        {
            // Posicionar al jugador en el planeta
            Vector3 directionToPlanet = (transform.position - currentPlanet.position).normalized;
            transform.position = currentPlanet.position + directionToPlanet * (currentPlanet.localScale.x / 2);

            // Girar el jugador hacia el planeta
            Vector3 gravityDirection = (currentPlanet.position - transform.position).normalized;
            float angle = Mathf.Atan2(gravityDirection.y, gravityDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + 90);
        }

        HandleMovementAnimations(); // Controlar las animaciones de movimiento

        UpdateUI(); // Actualizar la UI en cada frame
    }

    void Jump()
    {
        enPlaneta = false; // El jugador ya no está en un planeta
        transform.SetParent(null); // Desvincular al jugador del planeta

        Vector2 jumpDirection = Vector2.zero; // Dirección del salto
        if (Input.GetKey(KeyCode.A))
            jumpDirection = Vector2.left; // Saltar hacia la izquierda
        if (Input.GetKey(KeyCode.D))
            jumpDirection = Vector2.right; // Saltar hacia la derecha
        if (Input.GetKey(KeyCode.W))
            jumpDirection = Vector2.up; // Saltar hacia arriba
        if (Input.GetKey(KeyCode.S))
            jumpDirection = Vector2.down; // Saltar hacia abajo

        if (jumpDirection == Vector2.zero)
            jumpDirection = Vector2.up; // Si no se presiona ninguna tecla, saltar hacia arriba

        rb.velocity = Vector2.zero; // Reiniciar la velocidad

        float appliedJumpForce = enPlaneta ? jumpForce : jumpForce * airJumpForceMultiplier; // Aplicar la fuerza de salto
        rb.AddForce(jumpDirection * appliedJumpForce, ForceMode2D.Impulse); // Añadir fuerza al salto

        // Cambiar al estado de salto en el Animator
        animator.SetBool("isJumping", true);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisión con: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Planet"))
        {
            enPlaneta = true; // El jugador está en un planeta
            rb.velocity = Vector2.zero; // Reiniciar la velocidad
            currentPlanet = collision.transform; // Establecer el planeta actual
            transform.SetParent(currentPlanet); // Vincular al jugador al planeta

            // Cambiar el estado de vuelta a caminar
            animator.SetBool("isJumping", false);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Has tocado un enemigo");
            ReduceLife(); // Reduce la vida al tocar un enemigo

            // Verificar si el jugador tiene vidas restantes
            if (GameController.Instance.vidas > 0)
            {
                Debug.Log("Reiniciando la posición del jugador...");
                transform.position = posicionInicial; // Reiniciar la posición del jugador
                rb.velocity = Vector2.zero; // Reiniciar la velocidad
                enPlaneta = false; // El jugador ya no está en un planeta
                UpdateUI(); // Actualizar la UI después de perder una vida
            }
            else
            {
                // Aquí se manejan las acciones necesarias cuando el jugador no tiene vidas
                Debug.Log("Game Over - Cargando escena de Game Over...");
                MostrarGameOver(); // Mostrar la pantalla de Game Over
            }
        }

        // Si choca con la condición de ganar (ejemplo: llegar al objetivo final)
        if (collision.gameObject.CompareTag("WinCondition")) // Ajusta el tag según tu configuración
        {
            Debug.Log("Has ganado el juego - Cargando escena de Congratulations...");
            MostrarCongratulationsScene(); // Mostrar la pantalla de Congratulations
        }
    }

    public Vector3 GetInitialPosition()
    {
        return posicionInicial; // Retorna la posición inicial del jugador
    }

    // Método para sumar puntos
    public void SumarPuntos(int cantidad)
    {
        GameController.Instance.SumarPuntos(cantidad); // Acceder al GameController para sumar puntos
        UpdateUI(); // Actualizar la UI después de sumar puntos
    }

    void HandleMovementAnimations()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput != 0 && enPlaneta)
        {
            // El personaje está caminando
            animator.SetBool("isWalking", true);
        }
        else
        {
            // El personaje no está caminando
            animator.SetBool("isWalking", false);
        }
    }

    public void MostrarGameOver()
    {
        PlayerPrefs.GetInt(SCORE_KEY);
        PlayerPrefs.GetInt("PlayerLives", 0); // Al perder, guardar las vidas como 0
        SceneManager.LoadScene("GameOverScene"); // Cargar la escena de Game Over
    }




    void MostrarCongratulationsScene()
    {
        // Guarda el puntaje antes de cargar la escena de Congratulations
        PlayerPrefs.GetInt(SCORE_KEY);
        SceneManager.LoadScene("Congratulations"); // Carga la escena de Congratulations
    }

    void UpdateUI()
    {
        puntosTexto.text = "Score " + GameController.Instance.puntos; // Actualizar el texto de puntos

        // Actualizar la visibilidad de los corazones
        for (int i = 0; i < corazones.Length; i++)
        {
            if (i < GameController.Instance.vidas)
            {
                corazones[i].enabled = true; // Corazón visible
            }
            else
            {
                corazones[i].enabled = false; // Corazón "desactivado"
            }
        }
    }
}