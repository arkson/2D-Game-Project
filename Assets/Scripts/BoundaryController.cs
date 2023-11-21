using System;
using UnityEngine;


public class BoundaryController : MonoBehaviour {
	public GameObject ground;

	private Vector2 _boundaryMin;
	private Vector2 _boundaryMax;

	private Rigidbody2D _rigidBody;
	private PolygonCollider2D _currentFloor;
	private Move _moveBehavior;

	void Start() {
		_currentFloor = ground.GetComponent<PolygonCollider2D>();
		_rigidBody = GetComponent<Rigidbody2D>();
		_moveBehavior = GetComponent<Move>();
	}

	void OnTriggerExit2D(Collider2D floor) {
		_moveBehavior.inFloor = false;
		_moveBehavior.closestPoint = floor.ClosestPoint(_rigidBody.position);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		_moveBehavior.inFloor = true;
	}
}
