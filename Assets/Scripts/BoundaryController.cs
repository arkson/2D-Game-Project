using UnityEngine;

public class BoundaryController : MonoBehaviour
{
    public GameObject ground;

    private Vector2 _boundaryMin;
    private Vector2 _boundaryMax;

    private Rigidbody2D _rigidBody;
    private PolygonCollider2D _currentFloor;

    void Start()
    {
        _currentFloor = ground.GetComponent<PolygonCollider2D>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
    }
    
    void OnTriggerExit2D(Collider2D collision)
    {
        _rigidBody.position = collision.ClosestPoint(_rigidBody.position);
    }
}
