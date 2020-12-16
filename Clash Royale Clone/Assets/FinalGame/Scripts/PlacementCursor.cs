using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    float cursorSpeed = 8f;

    SpriteRenderer rend;

    GridJussi grid;
    Node[,] map;
    Node currentNode;
    List<Node.NodeState> playerSide;

    public bool blueLeftDestroyed = false;
    public bool blueRightDestroyed = false;
    public bool redLeftDestroyed = false;
    public bool redRightDestroyed = false;

    Node acceptableNode; //The Node which is the last acceptable Node placement-wise for the specific Unit/Spell.

    public GameObject placementShadow;

    public GameObject unitVar1;
    public GameObject unitVar2;
    public GameObject unitVar3;
    public GameObject unitVar4;
    public GameObject spellPlacementShadows;
    GameObject unitVar;

    public GameObject placer;
    public GameObject placerG;
    int playerLayer = 0;
    CardType.PlacementType placementType;    

    private void Awake() {
        unitVar = unitVar1;
        initialPos = initialTransform.position;
        gameObject.transform.position = initialPos;
        rend = GetComponent<SpriteRenderer>();
        rend.enabled = false;        
    }

    private void Start() {
        grid = FindObjectOfType<GridJussi>();
        map = grid.GetGrid();

        if (playerID == null) {
            playerSide = new List<Node.NodeState>();
            playerSide.Add(Node.NodeState.NoState);
        } else if (playerID.Equals("Player1")) {
            playerLayer = 14;
            playerSide = new List<Node.NodeState>();
            playerSide.Add(Node.NodeState.BlueBattlefield);
            playerSide.Add(Node.NodeState.RedIntrudedLeft);
            playerSide.Add(Node.NodeState.RedIntrudedRight);
        } else if (playerID.Equals("Player2")) {
            playerLayer = 13;
            playerSide = new List<Node.NodeState>();
            playerSide.Add(Node.NodeState.RedBattlefield);
            playerSide.Add(Node.NodeState.BlueIntrudedLeft);
            playerSide.Add(Node.NodeState.BlueIntrudedRight);
        }        

        unitVar.SetActive(false);
        spellPlacementShadows.SetActive(false);        
    }

    public void TowerDestroyed(int playerIndex) {
        if (playerID.Equals("Player1") && playerIndex == 1) {
            if(redLeftDestroyed && redRightDestroyed) {
                unitVar = unitVar4;
            } else if (redLeftDestroyed) {
                unitVar = unitVar2;
                playerSide.Add(Node.NodeState.BlueIntrudedLeft);
            } else if (redRightDestroyed) {
                unitVar = unitVar3;
                playerSide.Add(Node.NodeState.BlueIntrudedRight);
            } else {
                unitVar = unitVar1;
            }
        } else if (playerID.Equals("Player2") && playerIndex == 2) {
            if (blueLeftDestroyed && blueRightDestroyed) {
                unitVar = unitVar4;
            } else if (blueLeftDestroyed) {
                unitVar = unitVar2;
                playerSide.Add(Node.NodeState.RedIntrudedLeft);
            } else if (blueRightDestroyed) {
                unitVar = unitVar3;
                playerSide.Add(Node.NodeState.RedIntrudedRight);
            } else {
                unitVar = unitVar1;
            }
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
                        if(currentNode.nodeState != Node.NodeState.Obstacle && playerSide.Contains(currentNode.nodeState)) { //Units cannot be placed onto obstacles
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

        placer.layer = playerLayer;
        placerG.layer = playerLayer;

        ParticleSystem particles = placer.GetComponentInChildren<ParticleSystem>();
        if (particles != null) {
            particles.gameObject.layer = playerLayer;
        }

        if (placementType == CardType.PlacementType.Spell) {
           spellPlacementShadows.SetActive(true);
        } else {
           unitVar.SetActive(true);
        }
    }

    public void DeleteCursorObjects() {
        placer = null;
        placerG = null;
        unitVar.SetActive(false);
        spellPlacementShadows.SetActive(false);
        foreach (Transform child in transform) {
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
