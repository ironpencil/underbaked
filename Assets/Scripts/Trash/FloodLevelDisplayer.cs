using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloodLevelDisplayer : MonoBehaviour
{
    public Room room;
    Text text;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        room = GetComponentInParent<Room>();
        if (text == null) {
            Debug.Log("Text Null: " + transform.parent.name);
        }
        if (room == null) {
            Debug.Log("Room Null: " + transform.parent.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        text.text = Mathf.RoundToInt(room.GetWaterValue()).ToString();
    }
}
