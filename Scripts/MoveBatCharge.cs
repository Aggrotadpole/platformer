using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveBatCharge : Move
{
    public new float maxtimer = 1;
    new public float maxcooldown = 3;
    float basespeed = 1;
    public float speed = 1;
    
    public Vector3 pos1;
    public Vector3 pos2;
    Vector3 targetpos;

    public override void Use()
    {
        Strt();
        Debug.Log("Bat Charge was used!");
        isUsed = true;
        inUse = true;
        ent.anim.Play("Melee");
    }

    public override void Strt()
    {
        timer = maxtimer;
        targetpos = target.transform.position;
        speed = ent.speed;
        pos1 = ent.transform.position;
    }

   

    public override void Upd()
    {
        Cooldown();
        if (inUse == true && isUsed == false)
        {
            this.Use();
            
        }
        if (timer <= 0)
        {
            EndMove(this);
            timer = maxtimer;
        }
        if (inUse == true)
        {
            if (Vector2.Distance(ent.transform.position, targetpos) > speed)
            {
                Vector3 newvel = Vector3.MoveTowards(pos1, targetpos, speed) - pos1;
                ent.movevel += new Vector2(newvel.x,newvel.y);
                pos2 = newvel;
                timer -= Time.fixedDeltaTime;
                
            }
            else
            {
                Vector3 newvel = Vector3.MoveTowards(pos1, targetpos, speed) - pos1;
                ent.movevel += new Vector2(newvel.x, newvel.y);
                pos2 = newvel;
                EndMove(this);
            }
        }
        else
        {

        }
        
    }

    privat

    private void OnCollisionStay2D(Collision2D o)
    {
        if (inUse == true && o.gameObject.GetComponent<EntityLiving>() != null)
        {
            o.gameObject.GetComponent<EntityLiving>().Damage(ent.power, ent, new Vector2(Mathf.Abs(ent.power) * Mathf.Clamp(ent.vel.x, -1, 1) * 100, 64));
            ent.sound.clip = Resources.Load<AudioClip>("sounds/fx_tackle");
            ent.sound.Play();
            EndMove(this);
        }
        
    }
    public void OnCollisionEnter2D(Collision2D o)
    {
        if(inUse == true && o.gameObject.GetComponent<EntityLiving>() != null)
        {
            o.gameObject.GetComponent<EntityLiving>().Damage(ent.power,ent,new Vector2(Mathf.Abs(ent.power)*Mathf.Clamp(ent.vel.x,-1,1)*100,64));
            ent.sound.clip = Resources.Load<AudioClip>("sounds/fx_tackle");
            ent.sound.Play();
            EndMove(this);
        }
    }
}

