using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EntityPlayer : EntityLiving {
    
    
    
    // Use this for initialization

    

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
        if (isRunning == true)
        {
            speed = runspeed;
            moveanim = "Run";
        }
        else
        {
            speed = walkspeed;
            moveanim = "Walk";
        }
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
        vel = Vector2.Scale(vel, new Vector2(friction, friction));


        if (Input.GetKey(KeyCode.W))
        {
            if (!isTakingInv == true)
            {
                vel += new Vector2(0f, speed);
            }
            //vel += new Vector2(0f, speed * Time.fixedDeltaTime);

            //vel = Vector2.Lerp(vel, vel + new Vector2(0f, speed*100),100f);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isTakingInv == true)
            {
                isTakingInv = false;
            }
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (!isTakingInv == true)
            {
                movevel += new Vector2(0f, -speed);
            }

            //vel += new Vector2(0f, -speed * Time.fixedDeltaTime);
            //vel = Vector2.Lerp(vel, vel + new Vector2(0f, -speed), 10f);
            if (Input.GetKeyDown(KeyCode.K))
            {
                isTakingInv = !isTakingInv;
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (isTakingInv == true)
            {
                isTakingInv = false;
            }
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (!isTakingInv == true)
            {
                movevel += new Vector2(-speed, 0f);
                spr.flipX = true;
                anim.Play(moveanim);
                
            }
            //vel += new Vector2(-speed * Time.fixedDeltaTime, 0f);
            //vel = Vector2.Lerp(vel, vel + new Vector2(-speed, 0f), 100 / Time.fixedDeltaTime);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (isTakingInv == true)
            {
                currentItem--;
            }
            else { anim.Play(moveanim); }
        }

        if (Input.GetKeyUp(KeyCode.A))
        {

        }
        if (Input.GetKey(KeyCode.D))
        {
            if (!isTakingInv == true)
            {
                movevel += new Vector2(speed, 0f);
                spr.flipX = false;
                anim.Play(moveanim);
            }
            //vel += new Vector2(speed * Time.fixedDeltaTime, 0f);
            //vel = Vector2.Lerp(vel, vel + new Vector2(speed, 0f), 10/Time.fixedDeltaTime);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (isTakingInv == true)
            {
                currentItem++;
            }
            else { anim.Play(moveanim); }
        }

        if (Input.GetKeyUp(KeyCode.D))
        {

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded == true && isJumping == false)
            {
                jheightcurr = 0;
                this.Jump(jspeed,true);
                isJumping = true;
                jtimecurr = jtime;
                isGrounded = false;
            }
            else
            {

            }
            //vel = Vector2.Lerp(vel, vel + new Vector2(0f, speed*100),100f);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if(isJumping == true)
            {
                bool permitted = true;
                if (jtime > 0)
                {
                    if (jtimecurr > 0)
                    {
                        //permitted
                    }
                    else
                    {
                        isJumping = false;
                        permitted = false;
                    }
                }
                else
                {
                    //permitted
                }
                if (maxjheight > 0)
                {
                    if(jheightcurr < maxjheight)
                    {
                        //permitted
                    }
                    else
                    {
                        permitted = false;
                    }
                }
                else
                {
                    //permitted
                }
                if (permitted == true)
                {
                    this.Jump(postjspeed, false);
                    jtimecurr -= Time.fixedDeltaTime;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (!isTakingInv == true)
            {
                inventory[currentItem].OnUse(gameObject);
            }
            else
            {
                isTakingInv = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.J))
        {

        }
        if (Input.GetKey(KeyCode.L))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {

        }
        if (Input.GetKeyDown(KeyCode.I))
        {

        }


        if (movevel.x != 0)
        {
            //anim.Play("Walk");
        }
        else
        {
            anim.Play("Idle");
        }

        //vel = Vector2.Lerp(vel, new Vector2(0f, 0f), frictionm);

        HealthCalc();
        UpdatePhysics();
        CooldownMoves(1);
        UpdateMoves();
    }

    

    private void OnGUI()
    {
        
        GUI.Label(new Rect(0,0,200,200),"Hp: "+hp.ToString()+"/"+maxhp.ToString());
        GUI.Label(new Rect(0,16,200,200),"TAp: "+tap.ToString()+"/"+maxtap.ToString());

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

    


}
