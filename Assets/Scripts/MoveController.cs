using UnityEngine;


public delegate void FloorChangedHandler(int floorIndex);

public class MoveController : MonoBehaviour {
	public GameObject ground;

	[SerializeField] float hMaxSpeed;
	[SerializeField] float vMaxSpeed;
	private float _vertical, _horizontal;

	private Rigidbody2D _rigidBody;
	private PolygonCollider2D _currentFloor;

	public FloorChangedHandler OnFloorChanged1;
	public event FloorChangedHandler OnFloorChanged2;

	void Start() {
		_rigidBody = GetComponent<Rigidbody2D>();
		_currentFloor = ground.GetComponent<PolygonCollider2D>();

		OnFloorChanged1 += _ => { Debug.Log("OnFloorChanged1 handled"); };
		OnFloorChanged2 += _ => { Debug.Log("OnFloorChanged2 handled"); };

		OnFloorChanged1?.Invoke(0);
		OnFloorChanged2?.Invoke(0);
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

	Vector2 BoundToGround(Vector2 attemptedPosition) {
		return _currentFloor.OverlapPoint(attemptedPosition) ? attemptedPosition : _currentFloor.ClosestPoint(attemptedPosition);
	}
}
