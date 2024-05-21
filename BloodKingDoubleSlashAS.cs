using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BloodKingDoubleSlashAS : BehaviourTreeActionNode
{
    protected BehaviourTree behaviourTree;
    protected BloodKingBB bossBB;
    [SerializeField] private HitBox hitBox;
    
    private Rigidbody2D theRB;
    private Animator theAnim;

    [SerializeField] bool doFollowUpAtk;
    [SerializeField] private float pushStrength;
    private float pushDirect;
    [SerializeField] private float range;
    public float Range => range;

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
        pushDirect = (Utilities.BoolToInt(bossBB.facingRight) * 2) - 1;

        theAnim.SetTrigger("DoubleSlash");
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

    public void push()
    {
        theRB.velocity = new Vector2(pushStrength * pushDirect, 0);
    }

    public void halt()
    {
        theRB.velocity = Vector2.zero;
    }
}
