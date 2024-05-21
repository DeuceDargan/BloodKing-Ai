using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodKingRootState : BehaviourTreeRootNode
{
    protected BehaviourTree behaviourTree;
    protected BloodKingBB bossBB;

    private void Awake() 
    {
        behaviourTree = GetComponent<BehaviourTree>();
        bossBB = GetComponent<BloodKingBB>();
    }

    public override void Enter()
    {
        
    }

    public override void Tick()
    {
        // if intro hasnt played
        // play intro
        // go to intro state

        // else intro played
    }

    public override void GetActionState()
    {
        
        Debug.Log("Get Action Called");
        if (!isActive)
        {
            Debug.Log("Search Start");
            isActive = true;

            if (bossBB.eventStarted)
            {
                GetComponent<BloodKingDecideIntentSS>().GetActionState();
            }
            else
            {
                behaviourTree.AddAction(GetComponent<BloodKingAppearAS>());
                behaviourTree.AddAction(GetComponent<BloodKingIdleAS>());
                bossBB.lastActionAttack = false;
                bossBB.eventStarted = true;
                behaviourTree.SearchComplete();
            }
            
        }
    }

    public override void Exit()
    {

    }
}
