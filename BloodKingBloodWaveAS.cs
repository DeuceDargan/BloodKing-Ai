using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class BloodKingBloodWaveAS : BehaviourTreeActionNode
{
    protected BehaviourTree behaviourTree;
    protected BloodKingBB bossBB;
    [SerializeField] private ProjectileSpawner projectileSpawner;
    
    private Rigidbody2D theRB;
    private Animator theAnim;

    [SerializeField] private float range;
    public float Range => range;

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
        StartCoroutine(PlayerManager.instance.WarnPlayer());

        theAnim.SetTrigger("BloodWave");
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

    public void SpawnProjectiles()
    {
        projectileSpawner.SpawnProjectiles();
    }
}
