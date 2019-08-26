using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Ent_Recycler : Entity
{
    public int tradeAmt = 2;
    public Item[,] trades;
    public List<List<Item>> tradelist;
    // Start is called before the first frame update
    void Start()
    {
        //trades = new Item[tradeAmt,0];
        tradelist = new List<List<Item>>();
    }
    [ExecuteAlways]
    public void UpdateTradeList()
    {
        if(tradelist.Count != tradeAmt)
        {
            tradeAmt = tradelist.Count;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
       

    }
}
