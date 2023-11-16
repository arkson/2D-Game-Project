using UnityEngine;


[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(LineRenderer))]
public class ColliderPainter : MonoBehaviour {
	void Start() {
		var renderer = GetComponent<LineRenderer>();
		var polygons = GetComponents<PolygonCollider2D>();
		var count = 0;
		
		foreach (var polygon in polygons)
		{
			var path = polygon.GetPath(0);

			renderer.positionCount += path.Length;
			for (int i = 0; i < path.Length; i++) {
				renderer.SetPosition(count + i, path[i]);
			}
			count += path.Length;
		}
	}
}
