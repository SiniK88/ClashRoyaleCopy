using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

	public enum NodeState {
		Border, Battlefield, BlueIntrudedLeft, BlueIntrudedRight, RedIntrudedLeft, RedIntrudedRight, BlueBattlefield, RedBattlefield, Obstacle, NoState
    }

	public NodeState nodeState = NodeState.NoState;
	public Vector3 worldPosition;

	public Node(NodeState _nodeState, Vector3 _worldPos) {
		nodeState = _nodeState;
		worldPosition = _worldPos;
	}
}
