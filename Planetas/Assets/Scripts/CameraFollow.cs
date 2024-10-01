using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Referencia al objeto del jugador
    public float smoothSpeed = 0.125f; // Velocidad de seguimiento
    public Vector3 offset; // Desplazamiento de la cámara respecto al jugador

    void LateUpdate()
    {
        if (player == null) return; // Si el jugador es nulo, salir del método

        Vector3 desiredPosition = player.position + offset; // Nueva posición deseada de la cámara
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // Suavizar el movimiento
        transform.position = smoothedPosition; // Actualizar la posición de la cámara
    }
}
