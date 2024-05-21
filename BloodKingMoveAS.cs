using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodKingMoveAS : BehaviourTreeActionNode
{
    private enum MoveBehaviour
    {
        closeDist, openDist, moveTo
    };

    private MoveBehaviour moveBehaviour;
    protected BehaviourTree behaviourTree;
    protected BloodKingBB bossBB;

    private Rigidbody2D theRB;
    private Animator theAnim;

    private int direction;
    private float moveSpeed;
    private float activeMoveSpeed;

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
        direction = (Utilities.BoolToInt(bossBB.facingRight) * 2) - 1;
        transform.rotation = Quaternion.Euler(0, (Utilities.BoolToInt(bossBB.facingRight) * 180) - 180, 0);
        activeMoveSpeed = (moveSpeed * (1 - (bossBB.CurrentHealth / bossBB.maxHealth))) + moveSpeed;

        if (activeMoveSpeed > moveSpeed * 2)
        {
            activeMoveSpeed = moveSpeed * 2;
        }
        
        TriggerAnimation();
    }

    public override void Tick()
    {
        direction = (Utilities.BoolToInt(bossBB.facingRight) * 2) - 1;
        transform.rotation = Quaternion.Euler(0, (Utilities.BoolToInt(bossBB.facingRight) * 180) - 180, 0);

        switch (moveBehaviour)
        {
            case MoveBehaviour.closeDist:
                if (bossBB.DistFromTarget > bossBB.maxDist)
                {
                    theRB.velocity = new Vector2(direction * moveSpeed, 0);
                }
                else
                {
                    theRB.velocity = Vector2.zero;
                    theAnim.SetTrigger("ActionComplete");
                    isActive = false;
                }
                break;

            case MoveBehaviour.openDist:
                if (bossBB.DistFromTarget < bossBB.minDist && bossBB.distFromBackWall > 1)
                {
                    theRB.velocity = new Vector2(-direction * moveSpeed, 0);
                }
                else
                {
                    theRB.velocity = Vector2.zero;
                    theAnim.SetTrigger("ActionComplete");
                    isActive = false;
                }
                break;

            case MoveBehaviour.moveTo:
                break;
        }
    }

    public override void Exit()
    {

    }

    private void TriggerAnimation()
    {
        switch (moveBehaviour)
        {
            case MoveBehaviour.closeDist:
                if (bossBB.DistFromTarget > bossBB.maxDist)
                {
                    theAnim.SetTrigger("Run");
                }
                break;

            case MoveBehaviour.openDist:
                if (bossBB.DistFromTarget < bossBB.minDist && bossBB.distFromBackWall > 1)
                {
                    theAnim.SetTrigger("Retreat");
                }
                break;

            case MoveBehaviour.moveTo:
                break;
        }
    }

    public void SetMoveBehaviour (int i, float f)
    {
        if (i < System.Enum.GetValues(typeof(MoveBehaviour)).Length)
        {
            moveBehaviour = (MoveBehaviour)i;
            moveSpeed = f;
        }
    }
}
