using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodKingIdleAS : BehaviourTreeActionNode
{
    protected BehaviourTree behaviourTree;
    protected BloodKingBB bossBB;

    private Rigidbody2D theRB;
    private Animator theAnim;

    private float waitTime;
    private float waitCounter;

    private void Awake() 
    {
        behaviourTree = GetComponent<BehaviourTree>();
        bossBB = GetComponent<BloodKingBB>();

        theRB = GetComponent<Rigidbody2D>();
        theAnim = GetComponent<Animator>();
    }

    public override void Enter()
    {
        isActive = true;

        waitTime = (1f * ((float)bossBB.CurrentHealth / ((float)bossBB.maxHealth))) - .25f;

        bossBB.lastExcecutedAction = this;    
        theRB.velocity = Vector2.zero;
        
        waitCounter = waitTime;
    }

    public override void Tick()
    {
        transform.rotation = Quaternion.Euler(0, (Utilities.BoolToInt(bossBB.facingRight) * 180) - 180, 0);

        if (waitCounter <= 0)
        {
            isActive = false;
        }

        waitCounter -= Time.deltaTime;
    }

    public override void Exit()
    {
        
    }
}
