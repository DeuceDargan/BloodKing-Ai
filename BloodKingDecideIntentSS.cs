using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodKingDecideIntentSS : BehaviourTreeSetlectionNode
{
    protected BehaviourTree behaviourTree;
    protected BloodKingBB bossBB;

    private void Awake() 
    {
        behaviourTree = GetComponent<BehaviourTree>();
        bossBB = GetComponent<BloodKingBB>();
    }

    public override void GetActionState()
    {
        if (bossBB.lastActionAttack)
        {
            GetComponent<BloodKingDecideFollowUpSS>().GetActionState();
        }
        else
        {
            GetComponent<BloodKingDecideAttackSS>().GetActionState();
        }
    }
}
