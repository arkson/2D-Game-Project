using System;
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
	private PolygonCollider2D[] _availableFloors;
	public event FloorChangedHandler OnFloorChanged;

	void Start() {
		_rigidBody = GetComponent<Rigidbody2D>();
		_currentFloor = ground.GetComponent<PolygonCollider2D>();
		_availableFloors = ground.GetComponents<PolygonCollider2D>();
		OnFloorChanged?.Invoke(0);
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
			var nextIndex = Array.FindIndex(_availableFloors,
				floor => floor != _currentFloor && floor.OverlapPoint(attemptedPosition));
			if (nextIndex == -1)
			{
				return _currentFloor.ClosestPoint(attemptedPosition);
			}

			_currentFloor = _availableFloors[nextIndex];
			OnFloorChanged?.Invoke(nextIndex);
		}
		return attemptedPosition;
	}
}
