using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollisionChecker : MonoBehaviour
{
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Vector3 position = transform.position;
            Vector3 closestPoint = collision.collider.ClosestPoint(position);

            if (position.x > collision.collider.bounds.max.x || position.x < collision.collider.bounds.min.x)
                position.x = closestPoint.x;

            if (position.z > collision.collider.bounds.max.z || position.z < collision.collider.bounds.min.z)
                position.z = closestPoint.z;

            transform.position = position;
            Debug.Log("Collision checker working...");
        }
    }
}