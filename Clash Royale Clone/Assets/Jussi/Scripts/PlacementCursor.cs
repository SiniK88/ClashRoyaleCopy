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
    Vector3 initialPos = Vector3.zero + Vector3.forward * 5f;

    public Vector3 moveSpeed = Vector3.zero;
    float cursorSpeed = 5f;

    SpriteRenderer rend;

    private void Awake() {
        gameObject.transform.position = initialPos;
        rend = GetComponent<SpriteRenderer>();
        rend.enabled = false;
    }

    private void Update() {
        gameObject.transform.position += moveSpeed * cursorSpeed * Time.deltaTime;
    }

    public void MoveCursor(Vector2 move, string _playerID) {
        if (_playerID.Equals(playerID)) {
            moveSpeed = new Vector3(move.x, 0, move.y);
        }
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

    public void ResetCursor() {
        gameObject.transform.position = initialPos;
    }

    public void AddCursorObject(GameObject go) {
        Instantiate(go, gameObject.transform);
    }

    public void DeleteCursorObject() {
        foreach(Transform child in transform) {
            GameObject.Destroy(child.gameObject);
        }
    }
}
