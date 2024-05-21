using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodKingStunAS : BehaviourTreeActionNode
{
    protected BehaviourTree behaviourTree;
    protected BloodKingBB bossBB;

    [SerializeField] GameObject stunSR;
    private Rigidbody2D theRB;
    private Animator theAnim;

    public float waitTime;
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

        stunSR.SetActive(true);
        bossBB.lastExcecutedAction = this;    
        theRB.velocity = Vector2.zero;
        theAnim.SetTrigger("Idle");
        transform.rotation = Quaternion.Euler(0, (Utilities.BoolToInt(bossBB.facingRight) * 180) - 180, 0);
        
        waitCounter = waitTime;
    }

    public override void Tick()
    {
        

        if (waitCounter < 0)
        {
            isActive = false;
        }

        waitCounter -= Time.deltaTime;
    }

    public override void Exit()
    {
        stunSR.SetActive(false);
    }
}
