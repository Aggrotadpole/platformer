using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JobRank : ScriptableObject
{
    //Is this A rank? B rank? * rank?
    public string label;
    //The experience points rewarded to each individual
    public float expmin, expmax;
    //The team experience rewarded to the party
    public float texpmin, texpmax;
    //Effort needed to complete job VS reward for completing job
    public float effortmin, effortmax;
    public float rewardmin, rewardmax;
    //Effort needed to complete bonus job VS reward for completing bonus job
    //Bonus jobs are like extra/surprise parts of a mission or job
    public float effortbmin, effortbmax;
    public float rewardbmin, rewardbmax;
    //List of possible items as rewards
    public List<Object> rewards;
    //Whether or not this list uses rewards outside of this list
    public bool usesExternalRewards;
    //List of items not in the list that may still appear
    public List<Object> externalRewards;
    
}
