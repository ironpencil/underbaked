using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Map/Layout")]
public class MapLayout : ScriptableObject {

    public List<MapConnection> connections;
}
