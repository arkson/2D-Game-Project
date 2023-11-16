using UnityEngine;


[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(LineRenderer))]
public class ColliderPainter : MonoBehaviour {
	void Start() {
		var renderer = GetComponent<LineRenderer>();
		var polygon = GetComponent<PolygonCollider2D>();

		var path = polygon.GetPath(0);

		renderer.positionCount = path.Length;
		for (int i = 0; i < path.Length; i++) {
			renderer.SetPosition(i, path[i]);
		}
	}
}
