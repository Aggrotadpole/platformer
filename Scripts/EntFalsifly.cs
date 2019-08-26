using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntFalsifly : EntityLiving
{

    public EntityLiving target;
    public float noisetimer;
    public float noisechance = 0.002f;
    public bool active = false;
    public EntityLiving ent;
    public float timr = 2;
    public float maxcooldown = 3;
    public float cooldown = 0;
    public float speed = 10;
    public Vector3 pos1;
    public MoveBatCharge mb;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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


        moves[0] = new MoveBatCharge();
        
        
        moves[0] = gameObject.AddComponent<MoveBatCharge>();
        moves[0].ent = ent;
        moves[0].target = target;
        moves[0].Strt();
        mb = moves[0] as MoveBatCharge;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //mb.Upd();
        //moves[0] = mb;
        //cooldown = moves[0].cooldown;
        //timr = moves[0].timer;
        //active = moves[0].inUse;
        //ent = moves[0].ent;
        //pos1 = moves[0].target.transform.position;
        //moves[0].inUse = active;
        //moves[0].inUse = true;
        
        //CooldownMoves(1);
        //moves[0].Upd();
        
        

        if (active == false)
        {
            if (noisetimer <=0 && Random.value <= noisechance)
            {

                sound.clip = Resources.Load<AudioClip>("sounds/calls/tapslime_sleep");
                sound.Play();
                noisetimer = sound.clip.length;
                anim.Play("Idle");
            }
            
        }
        else
        {
            if (noisetimer <=0 && Random.value <= noisechance)
            {
                sound.clip = Resources.Load<AudioClip>("sounds/calls/tapslime" + Mathf.RoundToInt(Random.Range(1, 3)).ToString());
                sound.Play();
                noisetimer = sound.clip.length;
                anim.Play("Alert");
            }
        }
        
        noisetimer -= Time.fixedDeltaTime;
        UpdatePhysics();
        vel = Vector2.Scale(vel, new Vector2(friction, friction));




        HealthCalc();
        //CooldownMoves(1);
        if (moves[0].cooldown <= 0)
            moves[0].inUse = true;
        
        UpdateMoves();
    }
}
