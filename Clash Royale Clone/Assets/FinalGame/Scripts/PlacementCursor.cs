using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementCursor : MonoBehaviour {


    private void OnEnable() {
        PlayerController.OnPlacement += MoveCursor;
    }
    private void OnDisable() {
        PlayerController.OnPlacement -= MoveCursor;
    }

    public string playerID = null;
    public Vector3 currentPos;
    public Transform initialTransform;
    Vector3 initialPos;

    public Vector3 moveSpeed = Vector3.zero;
    float cursorSpeed = 6f;

    SpriteRenderer rend;

    GridJussi grid;
    Node[,] map;
    Node currentNode;
    Node.NodeState playerSide;
    Node acceptableNode; //The Node which is the last acceptable Node placement-wise for the specific Unit/Spell. 

    public GameObject placer;
    public GameObject placerG;
    CardType.PlacementType placementType;

    private void Awake() {
        initialPos = initialTransform.position;
        gameObject.transform.position = initialPos;
        rend = GetComponent<SpriteRenderer>();
        rend.enabled = false;        
    }

    private void Start() {
        grid = FindObjectOfType<GridJussi>();
        map = grid.GetGrid();

        if (playerID == null) {
            playerSide = Node.NodeState.NoState;
        } else if (playerID.Equals("Player1")) {
            playerSide = Node.NodeState.BlueBattlefield;
        } else if (playerID.Equals("Player2")) {
            playerSide = Node.NodeState.RedBattlefield;
        }
    }

    private void Update() {

        if(placer != null && placerG!= null) {
            

            
            placer.transform.position += moveSpeed * cursorSpeed * Time.deltaTime;

            UpdateNodes();
            placerG.transform.position = new Vector3(acceptableNode.worldPosition.x, acceptableNode.worldPosition.y, 0);

            if (currentNode.nodeState == Node.NodeState.Border || currentNode.nodeState == Node.NodeState.NoState) {
                placer.transform.position = currentPos;
            }

            currentPos = placer.transform.position;
            
        }       
    }

    public void UpdateNodes() {

        int x = Mathf.FloorToInt((placer.transform.position.x - grid.worldBottomLeft.x) / grid.nodeLengthX);
        int y = Mathf.FloorToInt((placer.transform.position.y - grid.worldBottomLeft.y) / grid.nodeLengthY);

        if (x >= 0 && x < map.GetLength(0) && y >= 0 && y < map.GetLength(1)) { //Check that the index is within map[x,y] array bounds.
            if (!(map[x, y] == currentNode)) { //Confirms that the new Node [x,y] is different than the previous, and therefore there is any reason to change anything
                currentNode = map[x, y];
                if(currentNode.nodeState != Node.NodeState.Border && currentNode.nodeState != Node.NodeState.NoState) { //If the new currentNode is border or outside the map, then the placermarker shouldn't update
                    if(placementType == CardType.PlacementType.Spell) {
                        acceptableNode = currentNode;
                    } else if (placementType == CardType.PlacementType.Unit) {
                        if(currentNode.nodeState != Node.NodeState.Obstacle && currentNode.nodeState == playerSide) { //Units cannot be placed onto obstacles
                            acceptableNode = currentNode;
                        }
                    }                                  
                }                
            }
        }
    }

    public void MoveCursor(Vector2 move, string _playerID) {
        if (_playerID.Equals(playerID)) {
            if (playerID.Equals("Player1")) {
                moveSpeed = new Vector3(move.x, move.y, 0);
            } else {
                moveSpeed = new Vector3(-move.x, -move.y, 0); //Invert the cursor movement, because Player2 (Red) is looking from opposite direction
            }
            
        }
    }

    public Vector3 GetNodePosition() {
        return placerG.transform.position;
    }

    public void ResetCursor() {
        gameObject.transform.position = initialPos;
    }

    public void AddCursorObject(GameObject placerMarker, GameObject placerGhost, CardType.PlacementType _placementType) {
       placementType = _placementType;
       placer = Instantiate(placerMarker, gameObject.transform);
       placerG = Instantiate(placerGhost, gameObject.transform);
    }

    public void DeleteCursorObjects() {
        placer = null;
        placerG = null;
        foreach(Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
    }

    //private void OnDrawGizmos() {
    //    Gizmos.color = Color.cyan;
    //    if (initialPos != null) {
    //        Gizmos.DrawWireSphere(initialPos, 0.5f);
    //    }
    //}
}
