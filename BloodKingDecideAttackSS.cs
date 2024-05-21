using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodKingDecideAttackSS : BehaviourTreeSetlectionNode
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
        bossBB.lastActionAttack = true;

        float meleeAtkRoll = Random.Range(0,100);
        float meleeAtkChance = (1 - (bossBB.DistFromTarget / 8)) * 100;
        int attackRoll = Random.Range(0,2);

        if (meleeAtkChance > meleeAtkRoll)
        {
            bossBB.prevAttack = BloodKingBB.PrevAttack.other;

            switch (attackRoll)
            {
                case 0:
                    bossBB.maxDist = GetComponent<BloodKingDoubleSlashAS>().Range / 2;
                    GetComponent<BloodKingMoveAS>().SetMoveBehaviour(0, 20);
                    behaviourTree.AddAction(GetComponent<BloodKingMoveAS>());
                    behaviourTree.AddAction(GetComponent<BloodKingDoubleSlashAS>());
                    bossBB.prevAttack = BloodKingBB.PrevAttack.other;
                    bossBB.doFollowUpAtk = true;
                    behaviourTree.SearchComplete();
                    break;

                case 1:
                    bossBB.maxDist = GetComponent<BloodKingChargeStabAS>().Range / 2;
                    GetComponent<BloodKingMoveAS>().SetMoveBehaviour(0, 20);
                    behaviourTree.AddAction(GetComponent<BloodKingMoveAS>());
                    behaviourTree.AddAction(GetComponent<BloodKingChargeStabAS>());
                    bossBB.prevAttack = BloodKingBB.PrevAttack.other;
                    bossBB.doFollowUpAtk = false;
                    behaviourTree.SearchComplete();
                    break;
            }
        }
        else
        {
            switch (attackRoll)
            {
                case 0:
                    bossBB.minDist = GetComponent<BloodKingTeleSlamAS>().Range;
                    GetComponent<BloodKingMoveAS>().SetMoveBehaviour(1, 20);
                    behaviourTree.AddAction(GetComponent<BloodKingMoveAS>());
                    behaviourTree.AddAction(GetComponent<BloodKingTeleSlamAS>());
                    bossBB.prevAttack = BloodKingBB.PrevAttack.teleSlam;
                    bossBB.doFollowUpAtk = true;
                    behaviourTree.SearchComplete();
                    break;

                case 1:
                    bossBB.minDist = GetComponent<BloodKingBloodWaveAS>().Range;
                    GetComponent<BloodKingMoveAS>().SetMoveBehaviour(1, 20);
                    behaviourTree.AddAction(GetComponent<BloodKingMoveAS>());
                    behaviourTree.AddAction(GetComponent<BloodKingBloodWaveAS>());
                    bossBB.prevAttack = BloodKingBB.PrevAttack.other;
                    bossBB.doFollowUpAtk = false;
                    behaviourTree.SearchComplete();
                    break;
            }
        }
    }
}
