using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodKingDecideFollowUpSS : BehaviourTreeSetlectionNode
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
        if (bossBB.doFollowUpAtk) // && bossBB.DistFromTarget < GetComponent<BloodKingJumpSlamAS>().Range)
        {
            switch (bossBB.prevAttack)
            {
                case BloodKingBB.PrevAttack.other:

                    if (bossBB.DistFromTarget < GetComponent<BloodKingJumpSlamAS>().Range)
                    {
                        behaviourTree.AddAction(GetComponent<BloodKingJumpSlamAS>());
                        bossBB.lastActionAttack = true;
                        bossBB.doFollowUpAtk = false;
                        behaviourTree.SearchComplete();
                    }
                    else
                    {
                        bossBB.minDist = 5;
                        GetComponent<BloodKingMoveAS>().SetMoveBehaviour(1, 15);
                        behaviourTree.AddAction(GetComponent<BloodKingMoveAS>());
                        behaviourTree.AddAction(GetComponent<BloodKingIdleAS>());
                        bossBB.lastActionAttack = false;
                        behaviourTree.SearchComplete();
                    }
                    
                    break;

                case BloodKingBB.PrevAttack.teleSlam:

                    if (bossBB.DistFromTarget < GetComponent<BloodKingDoubleSlashAS>().Range)
                    {
                        behaviourTree.AddAction(GetComponent<BloodKingDoubleSlashAS>());
                        bossBB.lastActionAttack = true;
                        bossBB.doFollowUpAtk = false;
                        behaviourTree.SearchComplete();
                    }
                    else
                    {
                        behaviourTree.AddAction(GetComponent<BloodKingChargeStabAS>());
                        bossBB.lastActionAttack = true;
                        bossBB.doFollowUpAtk = false;
                        behaviourTree.SearchComplete();
                    }

                    break;
            }
        }
        else
        {
            bossBB.minDist = 5;
            GetComponent<BloodKingMoveAS>().SetMoveBehaviour(1, 15);
            behaviourTree.AddAction(GetComponent<BloodKingMoveAS>());
            behaviourTree.AddAction(GetComponent<BloodKingIdleAS>());
            bossBB.lastActionAttack = false;
            behaviourTree.SearchComplete();
        }
        behaviourTree.SearchComplete();
    }
}
