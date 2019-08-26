using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    public bool breakable = true;
    public Vector2 size = new Vector2(1,1);
    public BoxCollider2D coll;
	// Use this for initialization
	void Start () {
        if (!gameObject.GetComponent<Collider2D>())
        {
            if (!coll)
                //coll = new BoxCollider2D();
                coll = gameObject.AddComponent<BoxCollider2D>();
            coll = gameObject.GetComponent<BoxCollider2D>();
            BoxCollider2D bcol = coll as BoxCollider2D;
            bcol.size = this.size;
            coll = bcol; 
        }
        



	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
