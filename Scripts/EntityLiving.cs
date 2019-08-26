using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EntityLiving : Entity {

    public float maxhp = 1f;
    public float hp = 1f;
    public bool isPersistent = false;
    public float exp = 0;
    public float expd = 1;
    public float maxtap = 1f;
    public float tap = 1f;
    public float power = 1f;
    public List<Move> moves;
    //public Move[] moves = new Move[2];
    public List<EntityLiving> targets;
    public List<EntityLiving> attackers;
    public float speed = 1f;
    public float walkspeed = 1.5f;
    public float runspeed = 2f;
    public string moveanim;
    public Vector2 movevel = Vector2.zero;
    public float maxenergy = 100;
    public float energy = 100;
    public float jspeed = 1;
    public float postjspeed = 0.5f;
    public float maxjheight = 5;
    public float jheightcurr = 0;
    public float jtime = 1;
    public float jtimecurr = 0;
    public bool isJumping = false;
    public bool isRunning = false;
    public Sprite hpSprite;
    public Item[] inventory;
    public int currentItem = 0;
    public bool isTakingInv = false;
    
    // Use this for initialization

    public void Jump(float h,bool isFirst)
    {
        movevel += new Vector2(0, h);
        jheightcurr += h;
        if (isFirst == true)
        {
            sound.clip = Resources.Load<AudioClip>("sounds/jump001");
            sound.Play();
        }
        
    }

    private void Awake()
    {
        canvas = GameObject.FindObjectOfType<Canvas>();
    }

    void Start() {
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

    

    // Update is called once per frame
    void FixedUpdate() {
        vel = Vector2.Scale(vel, new Vector2(friction, friction));

        if (isGrounded == false && isJumping == false && footCheck.GetComponent<FootCheck>().isTouchingGround == true) {
            isGrounded = true;
            if (Mathf.Abs(vel.y) >= 15)
            {
                sound.clip = Resources.Load<AudioClip>("sounds/land_hard");
            }
            else
            {
                sound.clip = Resources.Load<AudioClip>("sounds/land_med");
            }
            sound.Play();
        }
        if(isGrounded == true && footCheck.GetComponent<FootCheck>().isTouchingGround == false)
        {
            isGrounded = false;
        }


        UpdatePhysics();
        CooldownMoves(1);
        UpdateMoves();
    }

    public virtual void UpdatePhysics()
    {
        if (immovable == true)
        {
            vel = Vector2.zero;
        }
        //vel = new Vector2(vel.x*friction,vel.y*friction);
        rb.gravityScale = 0;
        //rb.gravityScale = gravity.y;
        //vel += gravity;


        //vel = Vector2.Lerp(vel, new Vector2(0f, 0f), frictionm);
        

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
        vel += movevel;
        vel -= gravity;
        movevel = Vector2.zero;
        //rb.gravityScale = gravity.y;
        rb.velocity = vel;
        vel = rb.velocity;
        HealthCalc();
    }

    public void UpdateMoves()
    {
        foreach(Move m in moves)
        {
            m.Upd();
        }
    }
    public virtual void CooldownMoves(float multiple)
    {
        foreach (Move m in moves)
        {
            System.Type type = typeof(Move);
            if (m.cooldown <= 0)
            {
                
            }
            else
            {
                m.cooldown -= multiple * Time.fixedDeltaTime;
            }
        }
    }

    private void OnGUI()
    {
        
    }
    void Ongui2(){
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
        hpgi.sprite = hpSprite;
    }

    public virtual void Heal(float amount,EntityLiving cause)
    {
        hp += amount;
        
    }

    public virtual void Damage(float amount, EntityLiving cause,Vector2 force)
    {
        hp -= amount;
        vel += force;
        if (cause!=null)
        {
            attackers.Add(cause);
        }
    }

    public virtual void Kill()
    {
        if (attackers != null)
        {
            for(int i = 0; i < attackers.Count; i++)
            {
                attackers[i].exp += expd;
            }
        }
        if (isPersistent == true)
        {

        }
        else
        {
            Debug.Log(name + " died!");
            Destroy(gameObject, 0);
        }
    }

    public virtual void HealthCalc()
    {
        if (hp <= 0)
        {
            this.Kill();
        }
    }


}
