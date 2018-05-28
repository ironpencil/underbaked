using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloodLevelDisplayer : MonoBehaviour {

    public Room room;
    Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        room = GetComponentInParent<Room>();
	}
	
	// Update is called once per frame
	void Update () {
        text.text = Mathf.RoundToInt(room.waterValue).ToString();
		
	}
}
