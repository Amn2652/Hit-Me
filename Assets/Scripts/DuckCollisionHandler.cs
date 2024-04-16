using UnityEngine;

public class DuckCollisionHandler: MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collider that the duck collided with has the tag "collider"
        if (collision.collider.CompareTag("collider"))
        {
            // Destroy the duck GameObject
            Destroy(gameObject);
        }
    }
}
