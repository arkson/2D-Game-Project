using UnityEngine;


[RequireComponent(typeof(PolygonCollider2D))]
public class ColliderPainter : MonoBehaviour {
	public GameObject prefab;

	private LineRenderer[] _renderers;

	void Start() {
		var polygons = GetComponents<PolygonCollider2D>();
		_renderers = new LineRenderer[polygons.Length];

		var moveController = GameObject.Find("Player")?.GetComponent<MoveController>();
		if (moveController)
		{
			var prevIndex = 0;
			moveController.OnFloorChanged += index =>
			{
				_renderers[prevIndex].startColor = _renderers[prevIndex].endColor = Color.blue;
				_renderers[index].startColor = _renderers[index].endColor = Color.red;
				prevIndex = index;
			};
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
