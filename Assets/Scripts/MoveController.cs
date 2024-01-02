using System.Linq;
using UnityEngine;


public delegate void FloorChangedHandler(int floorIndex);

public class MoveController : MonoBehaviour {
	public GameObject ground;

	[SerializeField] float hMaxSpeed;
	[SerializeField] float vMaxSpeed;
	private float _vertical, _horizontal;

	private Rigidbody2D _rigidBody;
	private PolygonCollider2D _currentFloor;
	public event FloorChangedHandler OnFloorChanged;

	void Start() {
		_rigidBody = GetComponent<Rigidbody2D>();
		_currentFloor = ground.GetComponent<PolygonCollider2D>();
	}

	void Update() {
		Move(Time.deltaTime);
	}

	void Move(float delta) {
		_horizontal = Input.GetAxisRaw("Horizontal");
		_vertical = Input.GetAxisRaw("Vertical");

		if (_horizontal == 0 && _vertical == 0) {
			return;
		}

		_rigidBody.position = BoundToGround(_rigidBody.position + new Vector2(_horizontal * hMaxSpeed, _vertical * vMaxSpeed) * delta);
	}

	Vector2 BoundToGround(Vector2 attemptedPosition)
	{
		var inFloor = _currentFloor.OverlapPoint(attemptedPosition);
		if (!inFloor)
		{
			var nextFloor = FindNextGround(attemptedPosition);
			if (!nextFloor)
			{
				return _currentFloor.ClosestPoint(attemptedPosition);

			}
			_currentFloor = nextFloor;
		}
		return attemptedPosition;
	}

	PolygonCollider2D FindNextGround(Vector2 attemptedPosition)
	{
		var floors = ground.GetComponents<PolygonCollider2D>();
		return floors.Where(floor => floor != _currentFloor).FirstOrDefault(floor => floor.OverlapPoint(attemptedPosition));
	}
}
