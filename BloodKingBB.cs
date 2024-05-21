using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodKingBB : BossBlackBoard
{
    public enum PrevAttack
    {
        other, teleSlam
    };

    public PrevAttack prevAttack;
    public BehaviourTreeActionNode lastExcecutedAction;

    private Rigidbody2D playerRB;

    public Transform lWall, rWall;
    private Transform backWall, frontWall;
    public Vector2 targetVel;
    
    public int playerMovementState;
    public float maxDist, minDist, moveTo, distFromBackWall, distFromFrontWall;
    public bool lastActionAttack, doFollowUpAtk, facingRight, isGrounded, isDead;

    private void Awake()
    {
        theRenderer = GetComponent<SpriteRenderer>();
        playerRB = target.GetComponent<Rigidbody2D>();
        playerController = target.GetComponent<PlayerController2DTA>();
        behaviourTree = GetComponent<BehaviourTree>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitializeStats();

        lastActionAttack = false;
        originalMat = theRenderer.material;
    }

    // Update is called once per frame  
    void Update()
    {
        UpdateStats();

        facingRight = target.transform.position.x > transform.position.x;

        playerMovementState = playerController.CurrentState;
        targetVel = playerRB.velocity;

        if (facingRight)
        {
            backWall = lWall;
            frontWall = rWall;
        }
        else
        {
            backWall = rWall;
            frontWall = lWall;
        }

        if (CurrentHealth <= 0 && !isDead)
        {
            behaviourTree.ForceAction(GetComponent<BloodKingDeadAS>());
            isDead = true;
        }

        distFromBackWall = Mathf.Abs(backWall.transform.position.x - transform.position.x);
        distFromFrontWall = Mathf.Abs(frontWall.transform.position.x - transform.position.x);
    }

    public void Destroy()
    {
        TimeManager.instance.StopTime(3);
        Destroy(gameObject);
    }

    public void ActivateHitBox()
    {
        BossHitBoxManager.instance.ActivateHitBox();
    }
}