using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BloodKingTeleSlamAS : BehaviourTreeActionNode
{
    private enum AnimState
    {
        charge, fall, end
    };

    protected BehaviourTree behaviourTree;
    protected BloodKingBB bossBB;
    private AnimState animState;
    [SerializeField] private HitBox hitBox;
    
    private Rigidbody2D theRB;
    private Animator theAnim;
    [SerializeField] private Transform groundPoint;

    [SerializeField] private float teleHeight;
    [SerializeField] private float launchSpeed;
    [SerializeField] private float arielSpeed;
    [SerializeField] private float chargeTime;
    private float chargeCounter;
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
        animState = AnimState.charge;
        chargeCounter = chargeTime;

        theAnim.SetTrigger("TeleSlam");
    }

    public override void Tick()
    {
        switch (animState)
        {
            case AnimState.charge:
                Charge();
                break;

            // case AnimState.pause:
            //     PauseLaunch();
            //     break;

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

    public void Charge()
    {
        chargeCounter -= Time.deltaTime;

        if (chargeCounter <= 0)
        {
            transform.position = new Vector3(bossBB.target.transform.position.x + (bossBB.targetVel.x * ((teleHeight - groundPoint.position.y) / launchSpeed)), teleHeight);
            AdvanceAnimState1();
            theAnim.SetTrigger("ChargeComplete");
        }
    }

    // public void PauseLaunch()
    // {
    //     // float arielVelocity = arielSpeed * arielDirection;
    //     theRB.velocity = Vector2.zero; //new Vector2(arielVelocity, 0);
    // }

    public void LaunchDown()
    {
        theRB.velocity = new Vector2(0, -launchSpeed);
    }

    public void EndLaunch()
    {
        theRB.velocity = Vector2.zero;
    }

    public void AdvanceAnimState1()
    {
        animState++;
    }
}
