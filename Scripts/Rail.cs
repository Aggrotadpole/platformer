using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class Rail : MonoBehaviour
{
    public RailNode[] nodes;
    public Sprite defaultSprite;
    public bool indRot;
    public bool turnAllNext;
    public bool stretchAllNext;

    public bool updatePlatformEffector = true;
    public float surfaceArc = 180;
    public float offset = 0;
    // Start is called before the first frame update
    private void Start()
    {
        nodes = GetComponentsInChildren<RailNode>();
    }

    public Vector3 LinearPosition(int seg,float ratio)
    {
        Vector3 p1 = nodes[seg].transform.position;
        Vector3 p2 = nodes[seg+1].transform.position;

        return Vector3.Lerp(p1,p2,ratio);
    }

    public Quaternion Orientation(int seg, float ratio)
    {
        Quaternion q1 = nodes[seg].transform.rotation;
        Quaternion q2 = nodes[seg+1].transform.rotation;
        if(nodes[seg].indRot == true)
        {
            return q1;
        }
        else
        {
            return Quaternion.Lerp(q1,q2,ratio);
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < nodes.Length - 1; i++)
        {
            Handles.DrawDottedLine(nodes[i].transform.position,nodes[i+1].transform.position,3.0f);
        }
    }

    private void Update()
    {
        nodes = GetComponentsInChildren<RailNode>();
        if (updatePlatformEffector == true) {
            for (int i = 0; i < nodes.Length - 1; i++)
            {
                
                nodes[i].pf.rotationalOffset = offset;
                nodes[i].pf.surfaceArc = surfaceArc;
                nodes[i].coll.usedByEffector = true;
            }
            updatePlatformEffector = false;
        }
            if (turnAllNext == true)
        {
            for (int i = 0; i < nodes.Length - 1; i++)
            {
                if (nodes[i].faceNext == true)
                {

                    Vector3 difference = nodes[i + 1].transform.position - nodes[i].transform.position;
                    float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
                    nodes[i].transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
                    //nodes[i].transform.LookAt(nodes[i + 1].transform.position);
                }

            }
            turnAllNext = false;
        }
        if (stretchAllNext == true)
        {
            for (int i = 0; i < nodes.Length - 1; i++)
            {
                if (nodes[i].stretchNext == true)
                {

                    float difference = Vector3.Distance(nodes[i].transform.position,nodes[i+1].transform.position);
                    nodes[i].length = difference;
                    nodes[i].Stretch();
                }

            }
            stretchAllNext = false;
        }
    }

}
