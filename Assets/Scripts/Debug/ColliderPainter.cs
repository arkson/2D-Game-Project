using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(PolygonCollider2D))]
public class ColliderPainter : MonoBehaviour
{

	public GameObject prefab;
	void Start() {
		var polygon = GetComponent<PolygonCollider2D>();
		var floor = Instantiate(prefab);
		var renderer = floor.GetComponent<LineRenderer>();

		var path = polygon.GetPath(0);

		renderer.positionCount = path.Length;
		for (int i = 0; i < path.Length; i++) {
			renderer.SetPosition(i, path[i]);
		}
	}
}
