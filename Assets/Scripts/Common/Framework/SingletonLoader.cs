using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonLoader : Singleton<SingletonLoader> {

    public List<GameObject> gameObjects;
    public bool setChildren = false;
    public bool destroySelf = true;

	// Use this for initialization
	public override void Start () {
        transform.parent = null;

        base.Start();
        if (this == null) { return; }

        Transform parent = null;

        if (setChildren)
        {
            parent = transform;
            destroySelf = false;
        }

        foreach (GameObject go in gameObjects)
        {
            Instantiate(go, parent);
        }

        if (destroySelf)
        {
            DestroyImmediate(gameObject);
        }
    }
}
