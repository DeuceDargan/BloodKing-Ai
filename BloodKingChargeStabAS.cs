using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BloodKingChargeStabAS : BehaviourTreeActionNode
{
    protected BehaviourTree behaviourTree;
    protected BloodKingBB bossBB;
    [SerializeField] private HitBox hitBox;
    
    private Rigidbody2D theRB;
    private Animator theAnim;

    [SerializeField] private float launchSpeed;
    [SerializeField] private float range;
    public float Range => range;

    private float chargeDirection;

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
        chargeDirection = (Utilities.BoolToInt(bossBB.facingRight) * 2) - 1;
        StartCoroutine(PlayerManager.instance.WarnPlayer());

        theAnim.SetTrigger("Stab");
    }

    public override void Tick()
    {
        if (bossBB.distFromFrontWall < 1)
        {
            theRB.velocity = Vector2.zero;
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

    public void Launch()
    {
        float launchVelocity = launchSpeed * chargeDirection;
        theRB.velocity = new Vector2(launchVelocity, 0);
    }

    public void LaunchBack()
    {
        float launchVelocity = launchSpeed * -chargeDirection / 3f;
        theRB.velocity = new Vector2(launchVelocity, 0);
    }

    public void EndLaunch()
    {
        theRB.velocity = Vector2.zero;
    }
}
