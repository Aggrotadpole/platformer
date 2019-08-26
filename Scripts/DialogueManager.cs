using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueManager : MonoBehaviour
{
    public bool isShowing = true;
    public bool isTalking = false;
    bool writing = false;
    public GUIStyle style;
     string str;
    public string intx;
    public string outtx;
    public float speed;
    public float multiplier;

    // Start is called before the first frame update
    void Start()
    {

        //style = new GUIStyle();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(Vector3.one,Space.Self);
    }
    public void OnGUI()
    {
        
        if (isShowing == true)
        {
           
            //GUI.Box(new Rect(64, 64, 160, 420), Resources.Load<Texture>("Sprite/UI/DLB"));
            //GUI.DrawTexture(new Rect(64, 64, 160, 420), Resources.Load<Texture>("Sprite/UI/DLB"));
            //GUILayout.Box(Resources.Load<Texture>("Sprite/UI/DLB"));
            //GUILayout.Box(Resources.Load<Texture>("Sprite/UI/DLB"),GUILayout.Width(320));
            GUILayout.Label(outtx);
        }
        if(isTalking == true && writing == false)
        {
            writing = true;
            NewText(intx);

        }
        
    }

    void NewText(string tx)
    {
        str = tx;
        outtx = "";
        StartCoroutine("PlayText");
    }

    IEnumerator PlayText()
    {
        foreach (char c in str)
        {
            if (outtx.Contains(str) || intx.Length <= outtx.Length)
            {
                writing = false;
                StopCoroutine("PlayText");
            }
            else
            {
                outtx += c;
                yield return new WaitForSeconds(speed * multiplier);
            }
        }

        writing = false;
    }
}
