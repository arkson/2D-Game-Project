using UnityEngine;

public class BoundaryController : MonoBehaviour
{
    public Transform floor; // Reference to the Floor's Transform

    private Vector2 _boundaryMin;
    private Vector2 _boundaryMax;

    private Rigidbody2D _rigidBody;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();

        if (floor != null)
        {
            // Calculate the boundaries based on the Collider of the Floor
            PolygonCollider2D boundaryCollider = floor.GetComponent<PolygonCollider2D>();
            if (boundaryCollider != null)
            {
                _boundaryMin = boundaryCollider.bounds.min;
                _boundaryMax = boundaryCollider.bounds.max;
            }
        }
    }

    void Update()
    {
        // Keep the Player within the boundary
        Vector2 clampedPosition = new Vector2(
            Mathf.Clamp(_rigidBody.position.x, _boundaryMin.x, _boundaryMax.x),
            Mathf.Clamp(_rigidBody.position.y, _boundaryMin.y, _boundaryMax.y)
        );

        _rigidBody.position = clampedPosition;
    }
}
