using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour {

    public delegate void Highlighted(GameObject _newObject);
    public static event Highlighted HighlightActivated;

    public delegate void DeHighlighted(GameObject _newObject);
    public static event DeHighlighted HighlightDeActivated;

    private void OnEnable() {
        PlayerInput.OnSelect += SelectObject;        
        PlayerInput.OnNavigate += NavigateList;
        //PlayerInput.OnPlacement += Placement;
        //PlayerInput.OnCancel += Cancel;
    }

    private void OnDisable() {
        PlayerInput.OnSelect -= SelectObject;        
        PlayerInput.OnNavigate -= NavigateList;
        //PlayerInput.OnPlacement -= Placement;
        //PlayerInput.OnCancel -= Cancel;
    }

    List<GameObject> gameObjects;
    public int currentIndex = 0;

    public bool selected = false;
    public GameObject oldObject;
    public GameObject newObject;

    private void Awake() {
        gameObjects = new List<GameObject>();
        CreateListOfObjects();
        SortListByX();
        newObject = gameObjects[currentIndex];
    }

    public void NavigateList(int i) {
        if (!selected) {            
            if (i == -1 && currentIndex != 0) {
                oldObject = gameObjects[currentIndex];
                currentIndex--;
                HighlightDeActivated(gameObjects[currentIndex - i]);
            } else if (i == 1 && currentIndex != gameObjects.Count - 1) {
                oldObject = gameObjects[currentIndex];
                currentIndex++;
                HighlightDeActivated(gameObjects[currentIndex - i]);
            }
            newObject = gameObjects[currentIndex];
            HighlightActivated(newObject); //Transfer of responsibility happens here. Now each gameObject should know, if he is selected, and act accordingly.
            print(newObject.name);
        }        
    }

    public void SelectObject() {

        selected = !selected;        
    }

    public void CreateListOfObjects() {

        gameObjects.Clear();
        var array = GameObject.FindGameObjectsWithTag("Card");
        gameObjects.AddRange(array);        
    }

    public void SortListByX() {

        for (int i = 0; i < gameObjects.Count; i++) {
            float currentX = gameObjects[i].transform.position.x;
            for (int j = i; j > 0; j--) { //sorting algorithm, based on the x-position. Index 0 should have the lowest x-pos, etc.               
                float previousX = gameObjects[j - 1].transform.position.x;
                if (previousX < currentX) {
                    break; //otherwise we would loop through the whole list even if they are in order.
                }
                if (previousX > currentX) { //removes and then inserts the gameObject to the left (to the negative index number)
                    GameObject temporary = gameObjects[j];
                    gameObjects.RemoveAt(j);
                    gameObjects.Insert(j - 1, temporary);
                }
            }
        }
    }
}
