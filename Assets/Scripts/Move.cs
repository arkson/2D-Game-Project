using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move : MonoBehaviour {
	public Collider2D floor;

	[SerializeField] float hMaxSpeed;
	[SerializeField] float vMaxSpeed;
	private float _vertical, _horizontal;
	private Rigidbody2D _rigidBody;
	private PolygonCollider2D _currentFloor;

	public bool inFloor;


	void Start() {
		_rigidBody = GetComponent<Rigidbody2D>();
	}

	void Update() {
		MovePlayer(Time.deltaTime);
	}

	void MovePlayer(float delta) {
		_horizontal = Input.GetAxisRaw("Horizontal");
		_vertical = Input.GetAxisRaw("Vertical");

		var attemptedPosition = _rigidBody.position + new Vector2(_horizontal * hMaxSpeed, _vertical * vMaxSpeed) * delta;

		if (_horizontal == 0 && _vertical == 0) {
			return;
		}

		if (inFloor) {
			_rigidBody.position = attemptedPosition;
		} else {
			var closestPoint = floor.ClosestPoint(attemptedPosition);
			var currentDistance = Vector2.Distance(closestPoint, _rigidBody.position);
			var attemptedDistance = Vector2.Distance(closestPoint, attemptedPosition);

			_rigidBody.position = Vector2.Lerp(_rigidBody.position, closestPoint, attemptedDistance / currentDistance);
		}
	}
}
