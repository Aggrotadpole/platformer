using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// attach to UI Text component (with the full text already there)

public class Typewriter : MonoBehaviour 
{

	Text txt;
    string str;
    public float speed = 0.1f;
    public float multiplier = 1f;

    void NewText(string tx)
    {
        str = tx;
        txt.text = "";
        StartCoroutine("PlayText");
    }

    void Awake()
    {
    txt = GetComponent<Text>();
    str = txt.text;
    txt.text = "";

    // TODO: add optional delay when to start
    StartCoroutine("PlayText");
    }

    IEnumerator PlayText()
    {
    foreach (char c in str)
    {
            if (txt.text.Contains(str))
            {
                
                StopCoroutine("PlayText");
            }
            else { 
                txt.text += c;
                yield return new WaitForSeconds(speed * multiplier);
            }
    }
    }

}
