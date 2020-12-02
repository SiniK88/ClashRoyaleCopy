using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GridJussi : MonoBehaviour {

	public Vector2 gridWorldSize;
	public Vector3 worldBottomLeft;

	public float nodeLengthX;
	public float nodeLengthY;
	Node[,] grid;

	

	int gridSizeX, gridSizeY;

	void Start() {
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeLengthX);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeLengthY);
		CreateGrid();
	}

	public Node[,] GetGrid() {
		return grid;
    }

	void CreateGrid() {
		grid = new Node[gridSizeX, gridSizeY];
		worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

		var nodeStates = new Node.NodeState[] { Node.NodeState.Border, Node.NodeState.Obstacle, Node.NodeState.Battlefield };
		var layers = new int[] { 10, 9, 8 };

		for (int x = 0; x < gridSizeX; x++) {
			for (int y = 0; y < gridSizeY; y++) {
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeLengthX + nodeLengthX/2) + Vector3.up * (y * nodeLengthY + nodeLengthY/2);
				var nodeState = Node.NodeState.NoState;

				Collider2D[] colliders = Physics2D.OverlapBoxAll(worldPoint, new Vector3(nodeLengthX, nodeLengthY), 0);
				if (colliders.Length != 0) {
					int layerMax = colliders.Max(c => c.gameObject.layer); // 8 is "Battlefield		9 is "Obstacle"		10 is "Border", finds the maximum value among these

					int i = Array.IndexOf(layers, layerMax);
					nodeState = (Node.NodeState)nodeStates.GetValue(i);
				}
				grid[x, y] = new Node(nodeState, worldPoint);
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

				Color cubeColor = Color.white;

				switch (n.nodeState) {
					case Node.NodeState.Border:
						cubeColor = Color.black;
						break;
					case Node.NodeState.Battlefield:
						cubeColor = Color.yellow;
						break;
					case Node.NodeState.Obstacle:
						cubeColor = Color.red;
						break;
					case Node.NodeState.NoState:
						cubeColor = Color.magenta;
						break;
				}

				Gizmos.color = cubeColor;
				Gizmos.DrawWireCube(n.worldPosition, new Vector3(nodeLengthX, nodeLengthY, 1));
			}
		}
	}
}

