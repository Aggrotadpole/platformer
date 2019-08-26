using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailMove : MonoBehaviour
{

    public Rail rail;
    public bool isConnected = false;
    public bool setSpeed = true;
    public float speed;
    public int current;
    private float transition;
    private bool isComplete;
    public bool useEnt = false;
    public Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!rail)
        {
            return;
        }
        if(isConnected == true) { 
        if (!isComplete)
        {
            Play();
        }
        else
        {
                //transform.rotation = Quaternion.identity;
                isComplete = false;
                current = 0;
                isConnected = false;
        }
    }
    }

    private void Play()
    {
        if (setSpeed)
        {
            transition += Time.fixedDeltaTime * speed;
        }
        else
        {
            if (useEnt == true) {
                velocity=gameObject.GetComponent<Entity>().vel;
            }
            
                transition += Time.fixedDeltaTime * velocity.magnitude;
            
        }
        if(transition > 1)
        {
            transition = 0;
            current++;
        }
        else if(transition < 0)
        {
            transition = 1;
            current--;
        }
        if(current >= rail.nodes.Length - 1){
            isComplete = true;
        }
        transform.position = rail.LinearPosition(current, transition);
        transform.rotation = rail.Orientation(current, transition);
        Vector3 rot = transform.rotation.eulerAngles;
        //use this to use rail like a water slide
        //transform.rotation = Quaternion.Euler(0, 0, rot.y);
        //Use this for rail grinding
        transform.rotation = Quaternion.Euler(0, 0, rot.x);
        //???
        //transform.rotation = Quaternion.Euler(0, 0, rot.z);
    }
}
