using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public GameObject explosionVFX;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("¡El jugador ha tocado un enemigo!");
            Instantiate(explosionVFX, transform.position, Quaternion.identity);
        }
    }
}
