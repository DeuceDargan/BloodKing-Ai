using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BloodKingJumpSlamAS : BehaviourTreeActionNode
{
    private enum AnimState
    {
        startup, launch, pause, fall, end
    };

    protected BehaviourTree behaviourTree;
    protected BloodKingBB bossBB;
    private AnimState animState;
    [SerializeField] private HitBox hitBox;
    
    private Rigidbody2D theRB;
    private Animator theAnim;

    [SerializeField] private float launchSpeed;
    [SerializeField] private float arielSpeed;
    [SerializeField] private float range;
    public float Range => range;

    private float arielDirection;

    private void Awake() 
    {
        behaviourTree = GetComponent<BehaviourTree>();
        bossBB = GetComponent<BloodKingBB>();

        theRB = GetComponent<Rigidbody2D>();
        theAnim = GetComponent<Animator>();
    }

    private void Start()
    {
        BossHitBoxManager.instance.AddHitBox(this, hitBox);
    }

    public override void Enter()
    {
        isActive = true;

        bossBB.lastExcecutedAction = this;
        theRB.velocity = Vector2.zero;
        transform.rotation = Quaternion.Euler(0, (Utilities.BoolToInt(bossBB.facingRight) * 180) - 180, 0);
        arielDirection = (Utilities.BoolToInt(bossBB.facingRight) * 2) - 1;
        animState = AnimState.startup;

        theAnim.SetTrigger("JumpSlam");
    }

    public override void Tick()
    {
        switch (animState)
        {
            case AnimState.launch:
                LaunchUp();
                break;

            case AnimState.pause:
                PauseLaunch();
                break;

            case AnimState.fall:
                LaunchDown();
                break;
        }
    }

    public override void Exit()
    {
        
    }

    public override void ActionComplete()
    {
        isActive = false;
        theAnim.SetTrigger("ActionComplete");
    }

    public void LaunchUp()
    {
        float arielVelocity = arielSpeed * arielDirection;
        theRB.velocity = new Vector2(arielDirection, launchSpeed);
    }

    public void PauseLaunch()
    {
        theRB.velocity = new Vector2(theRB.velocity.x, 0);
    }

    public void LaunchDown()
    {
        theRB.velocity = new Vector2(0, -launchSpeed);
    }

    public void EndLaunch()
    {
        theRB.velocity = Vector2.zero;
    }

    public void AdvanceAnimState()
    {
        animState++;
    }
}
