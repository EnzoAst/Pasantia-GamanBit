using UnityEngine;

public class SpaceShipController : MonoBehaviour
{
    public GameObject projectilePrefab;  // Prefab del proyectil
    public Transform firePoint;          // Punto desde donde disparas
    public float projectileSpeed = 10f;  // Velocidad del proyectil
    public float fireRate = 0.5f;        // Tiempo entre disparos

    private float nextFireTime = 0f;

    void Update()
    {
        // Dispara continuamente si ha pasado el tiempo de disparo
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate; // Actualizar el tiempo del próximo disparo
            Shoot(); // Disparar
        }
    }

    private void Shoot()
    {
        // Verificación para asegurar que el prefab esté asignado
        if (projectilePrefab == null)
        {
            Debug.LogError("El prefab del proyectil no está asignado.");
            return; // No disparamos si el prefab está vacío
        }

        // Instanciamos el proyectil
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        Debug.Log($"Disparando proyectil: {projectile.name}"); // Verifica el nombre del proyectil instanciado

        // Si no hay Rigidbody2D en el proyectil, mostramos un error
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("El proyectil no tiene un componente Rigidbody2D.");
            Destroy(projectile); // Destruir el proyectil si no tiene Rigidbody
            return;
        }

        // Aplicar la velocidad hacia abajo
        rb.velocity = firePoint.up * -projectileSpeed; // Disparar hacia abajo

        // Asegúrate de que el proyectil no sea destruido inmediatamente
        Debug.Log($"Instanciado: {projectile.name} en la posición: {firePoint.position}");
    }

}
