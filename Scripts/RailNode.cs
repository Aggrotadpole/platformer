using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class RailNode : MonoBehaviour
{
    public bool indRot = false;
    public bool faceNext = true;
    public bool stretchNext = true;
    public Vector3 rot;
    public Vector2 collNormal;
    public Quaternion r = new Quaternion();
    public Collider2D coll;
    public PlatformEffector2D pf = new PlatformEffector2D();
    public Vector2 offsetRatio = new Vector2(0.5f,0.5f);
    public float length = 0;
    private GameObject image;
    private SpriteRenderer spr;
    private Vector2 offsetCheck;
    // Start is called before the first frame update
    void Start()
    {
        
        
        image = gameObject.GetComponentInChildren<SpriteRenderer>().gameObject;
        if(image == null)
        {
            image = new GameObject();
            image.transform.SetParent(transform);
            spr = image.AddComponent<SpriteRenderer>();
            spr.sprite = GetComponentInParent<Rail>().defaultSprite;
        }
        coll = image.GetComponentInChildren<Collider2D>();
        if(coll == null)
        {
            coll = image.AddComponent<EdgeCollider2D>();
            
        }
        pf = image.GetComponentInChildren<PlatformEffector2D>();
        if (pf == null)
        {
            pf = image.AddComponent<PlatformEffector2D>();

        }
        image.transform.rotation = Quaternion.Euler(0, 0, 180);
        spr = image.GetComponent<SpriteRenderer>();
        
    }
    public void Stretch()
    {

        BoxCollider2D bcoll = coll as BoxCollider2D;
        if (bcoll) { 
        bcoll.offset = new Vector2(0, 0);
        bcoll.size = new Vector2(1, 1);
        image.transform.localPosition = new Vector2(image.transform.localScale.x * offsetRatio.x, image.transform.localScale.y * offsetRatio.y);
        image.transform.localScale = new Vector2(length, bcoll.size.y); }
            

        EdgeCollider2D ecoll = coll as EdgeCollider2D;
        if (ecoll) {
            ecoll.offset = new Vector2(offsetRatio.x,offsetRatio.y);
            ecoll.points = new Vector2[]{ new Vector2(0,0),new Vector2(-1,0)};
            image.transform.localPosition = new Vector2(image.transform.localScale.x * offsetRatio.x, image.transform.localScale.y * offsetRatio.y);
            image.transform.localScale = new Vector2(length, image.transform.localScale.y);
        }
        offsetCheck = offsetRatio;
        image.transform.localEulerAngles = new Vector3(0, 0, 180);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (coll == null)
        {
            coll = image.GetComponent<Collider2D>();
        }
        //rot = r.eulerAngles;
        if (transform.rotation.y != 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.z);

        }
        if (offsetCheck != offsetRatio)
        {
            Stretch();
        }
        }

    private void OnTriggerEnter2D(Collision2D c)
    {
        Entity e = c.collider.gameObject.GetComponent<Entity>();
        if (e is Entity)
        {
            RailMove rl = c.collider.gameObject.GetComponent<RailMove>();
            if (rl is RailMove)
            {
                if ( e.immovable == false)
                {
                    collNormal = c.GetContact(0).normal;
                }
                rl.current = transform.GetSiblingIndex();
                rl.isConnected = true;
            }

        }
    }
}
