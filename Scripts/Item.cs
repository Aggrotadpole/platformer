using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Entity{

    static float ID;
    Item iid;
    public int amount;
    public int maxAmount;
    public Texture2D tex;
    public string texpath;
    new public bool isGrabbable = true;
    static void UpdateTex()
    {
        //Texture2D t = (Texture2D)UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/Textures/texture.jpg", typeof(Texture2D));
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void ItemRegister(string nam, float id,float weight,int maxamount)
    {
       
    }

    public Item()
    {
        ItemRegister("ItemApple",0,1,1);
    }

    public void OnUse(GameObject user)
    {
        Debug.Log(user.name+" used "+ID+"!");
    }
	
	
}
