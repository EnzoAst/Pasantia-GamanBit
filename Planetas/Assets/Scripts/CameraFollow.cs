using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Referencia al objeto del jugador
    public float smoothSpeed = 0.125f; // Velocidad de seguimiento
    public Vector3 offset; // Desplazamiento de la c�mara respecto al jugador

    void LateUpdate()
    {
        if (player == null) return; // Si el jugador es nulo, salir del m�todo

        Vector3 desiredPosition = player.position + offset; // Nueva posici�n deseada de la c�mara
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Suavizar el movimiento
        transform.position = smoothedPosition; // Actualizar la posici�n de la c�mara
    }
}
