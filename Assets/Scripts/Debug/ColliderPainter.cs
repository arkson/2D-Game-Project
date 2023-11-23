using UnityEngine;


[RequireComponent(typeof(PolygonCollider2D))]
public class ColliderPainter : MonoBehaviour {
	public GameObject prefab;

	private LineRenderer[] _renderers;

	void Start() {
		var polygons = GetComponents<PolygonCollider2D>();
		_renderers = new LineRenderer[polygons.Length];

		var moveController = GameObject.Find("Player")?.GetComponent<MoveController>();
		if (moveController) {
			moveController.OnFloorChanged1 += index => { Debug.Log("handling floorChanged1 in painter"); };
			moveController.OnFloorChanged2 += index => { Debug.Log("handling floorChanged2 in painter"); };

			moveController.OnFloorChanged1(0); // raising from outside allowed
			// moveController.OnFloorChanged2(0); // raising from outside errors

			moveController.OnFloorChanged1 = null; // clearing subscribers from outside allowed
			// moveController.OnFloorChanged2 = null; // clearing subscribers from outside errors
		}

		var k = 0;
		foreach (var polygon in polygons) {
			var floor = Instantiate(prefab, transform);
			var renderer = floor.GetComponent<LineRenderer>();
			_renderers[k++] = renderer;
			var path = polygon.GetPath(0);

			renderer.positionCount = path.Length;
			for (int i = 0; i < path.Length; i++) {
				renderer.SetPosition(i, path[i]);
			}
		}
	}
}
