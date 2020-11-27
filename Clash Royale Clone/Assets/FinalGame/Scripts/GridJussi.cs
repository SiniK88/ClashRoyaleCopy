using UnityEngine;
using System.Collections;

public class GridJussi : MonoBehaviour {

	public LayerMask unplacableMask;
	public Vector2 gridWorldSize;
	public float nodeLengthX;
	public float nodeLengthY;
	Node[,] grid;

	int gridSizeX, gridSizeY;

	void Start() {
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeLengthX);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeLengthY);
		CreateGrid();
	}

	void CreateGrid() {
		grid = new Node[gridSizeX, gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

		for (int x = 0; x < gridSizeX; x++) {
			for (int y = 0; y < gridSizeY; y++) {
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeLengthX + nodeLengthX/2) + Vector3.up * (y * nodeLengthY + nodeLengthY/2);
				bool walkable = !(Physics.CheckBox(worldPoint, new Vector3(nodeLengthX/2, nodeLengthY/2), Quaternion.identity, unplacableMask));
				grid[x, y] = new Node(walkable, worldPoint);
			}
		}
	}

	public Node NodeFromWorldPoint(Vector3 worldPosition) {
		float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
		float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
		percentX = Mathf.Clamp01(percentX);
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
		return grid[x, y];
	}

	void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));


		if (grid != null) {
			foreach (Node n in grid) {
				Gizmos.color = (n.walkable) ? Color.white : Color.red;
				Gizmos.DrawCube(n.worldPosition, new Vector3(nodeLengthX, nodeLengthY, 1)*0.9f);
			}
		}
	}
}

