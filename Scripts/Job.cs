using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Job : ScriptableObject
{
    public string title = "---";
    public JobRank Rank;
    public float exp;
    public float texp;
    public List<Object> rewards;
    
}
