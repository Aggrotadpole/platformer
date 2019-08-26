using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveBloodSpray : Move
{
    public new float maxtimer = 1;
    new public float maxcooldown = 3;
    float basespeed = 1;
    public float speed = 1;
    
    public Vector3 pos1;
    Vector3 targetpos;

    public override void Use()
    {
        Strt();
        Debug.Log("Blood Spray was used!");
        isUsed = true;
        inUse = true;
        ent.anim.Play("Melee");
    }

    public override void Strt()
    {
        timer = maxtimer;
        targetpos = target.transform.position;
        
        
        pos1 = ent.transform.position;
    }

   

    public override void Upd()
    {
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
            if (Vector2.Distance(pos1, targetpos) != 0)
            {
                
                ent.transform.position = Vector3.MoveTowards(ent.transform.position, targetpos, speed * Time.fixedDeltaTime);
                timer -= Time.fixedDeltaTime;
            }
            else
            {
                EndMove(this);
            }
        }
        else
        {

        }
    }

}

