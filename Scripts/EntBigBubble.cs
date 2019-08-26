using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntBigBubble : EntityLiving
{
    public float vision = 4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D c)
    {
        EntityLiving e = c.gameObject.GetComponent<EntityLiving>();
        if (e is Entity)
        {
            if (e.immovable == false)
            {
                e.hp++;
                e.vel.Scale(new Vector2(1 / power, 1 / power));
            }
        }
    }
    private void OnTriggerExit2D(Collider2D c)
    {
        EntityLiving e = c.gameObject.GetComponent<EntityLiving>();
        if (e is EntityLiving)
        {
            if (e.immovable == false)
            {
                e.hp++;
                e.vel.Scale(new Vector2(1 / power, 1 / power));
            }
        }
    }
    private void OnTriggerStay2D(Collider2D c)
    {
        EntityLiving e = c.gameObject.GetComponent<EntityLiving>();
        if (e is EntityLiving)
        {
            if (e.immovable == false)
            {
                e.hp-=0.1f;
                c.transform.position = Vector2.Lerp(c.transform.position, transform.position, power * Time.deltaTime);
            }
        }
    }
}
