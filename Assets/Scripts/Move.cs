using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Move : MonoBehaviour {
	[SerializeField] float hMaxSpeed;
	[SerializeField] float vMaxSpeed;
	float _vertical, _horizontal;
	Rigidbody2D _rigidBody;

	public bool inFloor;
	public Vector2 closestPoint;

	void Start() {
		_rigidBody = GetComponent<Rigidbody2D>();
	}

	void Update() {
		MovePlayer();
	}

	void MovePlayer() {
		_horizontal = Input.GetAxisRaw("Horizontal");
		_vertical = Input.GetAxisRaw("Vertical");

		var attemptedPosition = _rigidBody.position + new Vector2(_horizontal * hMaxSpeed, _vertical * vMaxSpeed) * Time.deltaTime;

		if (inFloor) {
			_rigidBody.position = attemptedPosition;
		}
		else {
			_rigidBody.position = Vector2.Lerp(attemptedPosition, closestPoint, 0.01f);
		}
	}
}
