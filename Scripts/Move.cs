using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public EntityLiving ent;
    public bool inUse;
    public float maxtimer = 3;
    public float timer = 0;
    public float maxcooldown = 3;
    public float cooldown = 0;
    public EntityLiving target;
    public bool isUsed = false;

    public virtual void Strt()
    {
        Use();
    }

    public virtual void Use()
    {
        
        timer = 100;
    }

    public virtual void Upd()
    {
        if (timer <= 0)
        {
            //EndMove(this);
            timer = maxtimer;
        }
        if (inUse == true)
        {
            this.Use();
        }
        else
        {

        }
        Cooldown();
    }

    public virtual void Cooldown()
    {
        
        if (cooldown <= 0)
        {

        }
        else
        {
            cooldown -= Time.fixedDeltaTime;
        }
    }

    public void EndMove(Move m)
    {
        m.inUse = false;
        m.isUsed = false;
        cooldown = maxcooldown;
    }
}