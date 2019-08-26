using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;



public abstract class Entity : MonoBehaviour {
    public Canvas canvas;
    public float unitsize = 32;
    public float weight;
    public bool isGrabbable;
    public SpriteRenderer spr;
    public Rigidbody2D rb;
    public PhysicsMaterial2D material;
    public Collider2D coll;
    public Vector2 dimensions = new Vector2(32, 32);
    public Vector2 size = new Vector2(1, 1);
    public float maxspeed = 100;
    public float frictionm = 9f;
    public float friction = 0.9f;
    public Vector2 gravity = new Vector2(0, -1);
    public bool symmetrical = true;
    public Animator anim;
    public bool immovable = false;
    public bool isKine = false;
    public bool canRotate = false;
    public Vector2 vel = new Vector2(0, 0);
    public GameObject footCheck;
    public bool isGrounded = false;
    public Vector2 sizeRatio;
    public AudioSource sound; 
    // Use this for initialization

    

    private void Awake()
    {
        canvas = GameObject.FindObjectOfType<Canvas>();
    }

    void Start() {
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        sizeRatio = this.size * (this.dimensions / this.unitsize);
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        rb.sharedMaterial = material;
        if (!gameObject.GetComponent<Collider2D>())
        {
            if (!coll)
                coll = gameObject.AddComponent<BoxCollider2D>();
            coll = gameObject.GetComponent<BoxCollider2D>();
            BoxCollider2D bcol = coll as BoxCollider2D;
            bcol.size = sizeRatio;
            coll = bcol;
        }

        if (canRotate == false)
        {
            rb.freezeRotation = true;
        }
        else
        {
            rb.freezeRotation = false;
        }

        if (isKine == false)
        {
            rb.isKinematic = false;
        }
        else
        {
            rb.isKinematic = true;
        }
        footCheck = new GameObject();
        footCheck.transform.SetParent(gameObject.transform);
        footCheck.transform.position += new Vector3(0f,-0.5f,0f);
        CircleCollider2D foot = footCheck.AddComponent<CircleCollider2D>();
        footCheck.AddComponent<FootCheck>();
        foot.radius = 0.2f;
        coll.usedByEffector = false;
    }

    public Component[] FindCompInChildrenOf(string n,System.Type type)
    {
        Transform t = transform.Find(n);
        Component[] types = t.GetComponentsInChildren(type);
        return types;
    }

    // Update is called once per frame
    void FixedUpdate() {
        
        
        if (immovable == true)
            vel = Vector2.zero;

        vel = Vector2.Scale(vel, new Vector2(friction, friction));
        //vel = new Vector2(vel.x*friction,vel.y*friction);
        rb.gravityScale = 0;
        //rb.gravityScale = gravity.y;
        //vel += gravity;
        

        //vel = Vector2.Lerp(vel, new Vector2(0f, 0f), frictionm);

        if (immovable == true)
            vel = Vector2.zero;

        //rb.AddRelativeForce(vel);
        //rb.MovePosition(rb.position + vel);
        if (vel.x > maxspeed)
        {

            vel.x = maxspeed;
        }
        else if (vel.x < -maxspeed)
        {
            vel.x = -maxspeed;
        }
        if (vel.y > maxspeed)
        {

            vel.y = maxspeed;
        }
        else if (vel.y < -maxspeed)
        {
            vel.y = -maxspeed;
        }
        //vel = vel;
        //rb.velocity = vel;
        vel -= gravity;
        //rb.gravityScale = gravity.y;
        rb.velocity = vel;
        vel = rb.velocity;
        
    }

   

    private void OnGUI()
    {
        
    }
    void ongui2(){
        string guiElm = gameObject.GetInstanceID().ToString() + "gui";
        for (int i = 0; i < GameObject.FindGameObjectsWithTag(guiElm).Length; i++)
        {
            GameObject.Destroy(GameObject.FindGameObjectsWithTag(guiElm)[i]);
        }
        GameObject hpg = new GameObject();
        hpg.transform.SetParent(canvas.transform);
        hpg.tag = guiElm;
        hpg.AddComponent<CanvasRenderer>();
        RectTransform hpgt = hpg.AddComponent<RectTransform>();
        Image hpgi = hpg.AddComponent<Image>();
        hpgt.anchorMin = new Vector2(0f, 1f);
        hpgt.anchorMax = new Vector2(0f, 1f);
        hpgt.pivot = new Vector2(0f, 1f);
        hpgt.anchoredPosition = new Vector2(0f, 0f);
        hpgt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 64f);
        hpgt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 64f);
    }

  

}
