using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootCheck : MonoBehaviour {
    public CircleCollider2D coll;
    public LayerMask ground;
    
    public bool isTouchingGround;
    public Entity Ent;
    // Use this for initialization
    void Start () {
        coll = gameObject.GetComponent<CircleCollider2D>();
        coll.isTrigger = true;
        ground = LayerMask.GetMask("ground");
        Ent = transform.parent.GetComponent<Entity>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.position.Set(Ent.sizeRatio.x, Ent.sizeRatio.y, 0);
        
        isTouchingGround = Physics2D.OverlapCircle(transform.position, coll.radius, ground);
        
	}
}
