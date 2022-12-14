using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalScripts.entity;
using GlobalScripts.entity.ai;

public class Animation : DustEntity

{

    
 
    // Start is called before the first frame update
    public void Start()
    {
        base.Start();
        IdleAIGoals.Add(0, new RandomWalkGoal(this));
        IdleAIGoals.Add(1, new RandomWalkGoal(this));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
