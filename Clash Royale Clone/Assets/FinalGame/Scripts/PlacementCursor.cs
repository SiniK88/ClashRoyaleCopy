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
    Vector3 initialPos = Vector3.zero + Vector3.up * 5f;

    public Vector3 moveSpeed = Vector3.zero;
    float cursorSpeed = 5f;

    SpriteRenderer rend;

    Vector3 previousPos = Vector3.zero;

    GridJussi grid;
    Node[,] map;
    Node currentNode;
    Node acceptableNode; //The Node which is the last acceptable Node placement-wise for the specific Unit/Spell. 

    public GameObject placer;
    public GameObject placerG;

    private void Awake() {        
        gameObject.transform.position = initialPos;
        rend = GetComponent<SpriteRenderer>();
        rend.enabled = false;
        grid = FindObjectOfType<GridJussi>();
    }

    private void Start() {
        map = grid.GetGrid();
    }

    private void Update() {

        if(placer != null && placerG!= null) {
            
            UpdateNodes();

            placerG.transform.position = acceptableNode.worldPosition;
            placer.transform.position += moveSpeed * cursorSpeed * Time.deltaTime;
            
        }       
    }

    public void UpdateNodes() {

        int x = Mathf.FloorToInt((grid.gridWorldSize.x / 2 + placer.transform.position.x) / grid.nodeLengthX);
        int y = Mathf.FloorToInt((grid.gridWorldSize.y / 2 + placer.transform.position.y) / grid.nodeLengthY);

        if (x >= 0 && x < map.GetLength(0) && y >= 0 && y < map.GetLength(1)) { //Check that the index is within map[x,y] array bounds.
            if (!(map[x, y] == currentNode)) { //Confirms that the new Node [x,y] is different than the previous, and therefore there is any reason to change anything
                currentNode = map[x, y];
                if(currentNode.nodeState != Node.NodeState.Border) { //If the new currentNode is border, then the placermarker shouldn't update
                    acceptableNode = currentNode;                    
                }                
            }
        }
    }

    public void MoveCursor(Vector2 move, string _playerID) {
        if (_playerID.Equals(playerID)) {
            moveSpeed = new Vector3(move.x, move.y, 0);
        }
    }

    public Vector3 GetNodePosition() {
        return placerG.transform.position;
    }

    public void ResetCursor() {
        gameObject.transform.position = initialPos;
    }

    public void AddCursorObject(GameObject placerMarker, GameObject placerGhost) {
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
    //    if(currentNode != null) {
    //        Gizmos.DrawSphere(currentNode.worldPosition, 0.5f);
    //    }        
    //}
}
