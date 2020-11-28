using UnityEngine;
using System.Collections;

public class GridJussi : MonoBehaviour {

	public LayerMask borderMask;
	public LayerMask obstacleMask;
	public LayerMask battlefieldMask;

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

	public Node[,] GetGrid() {
		return grid;
    }

	void CreateGrid() {
		grid = new Node[gridSizeX, gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

		for (int x = 0; x < gridSizeX; x++) {
			for (int y = 0; y < gridSizeY; y++) {
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeLengthX + nodeLengthX/2) + Vector3.up * (y * nodeLengthY + nodeLengthY/2);

				Collider[] colliders = Physics.OverlapBox(worldPoint, new Vector3(nodeLengthX / 2, nodeLengthY / 2, 1));

				//Beware: Some horrible code ahead. All I want is to check the "Worst case scenario" and mark the node based on that... Too much to ask? Good luck managing layers after the following crap.

				int layerNumber = -1; // 8 is "Battlefield		9 is "Obstacle"		10 is "Border"

				foreach(Collider c in colliders) {
					int i = c.gameObject.layer;

					if(i > layerNumber) {
						layerNumber = i;
                    }
                }
								
				if(layerNumber == 10) {
					grid[x, y] = new Node(Node.NodeState.Border, worldPoint);
                } else if (layerNumber == 9){
					grid[x, y] = new Node(Node.NodeState.Obstacle, worldPoint);
				} else if (layerNumber == 8) {
					grid[x, y] = new Node(Node.NodeState.Battlefield, worldPoint);
				} else {
					grid[x, y] = new Node(Node.NodeState.NoState, worldPoint);
				}

				//phew, got past it! Now never mention this.
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
				Gizmos.DrawWireCube(n.worldPosition, new Vector3(nodeLengthX, nodeLengthY, 1)*0.9f);
			}
		}
	}
}

