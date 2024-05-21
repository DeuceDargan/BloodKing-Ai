using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BloodKingAppearAS : BehaviourTreeActionNode
{
    protected BehaviourTree behaviourTree;
    protected BloodKingBB bossBB;
    
    private Rigidbody2D theRB;
    private Animator theAnim;

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

        bossBB.lastExcecutedAction = this;
        theRB.velocity = Vector2.zero;
        transform.rotation = Quaternion.Euler(0, (Utilities.BoolToInt(bossBB.facingRight) * 180) - 180, 0);

        theAnim.SetTrigger("Appear");
    }

    public override void Tick()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void ActionComplete()
    {
        isActive = false;
        theAnim.SetTrigger("ActionComplete");
    }
}
