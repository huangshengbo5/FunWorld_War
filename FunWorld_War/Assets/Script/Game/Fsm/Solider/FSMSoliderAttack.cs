using System.Collections;
using System.Collections.Generic;
using GameFramework.Fsm;
using UnityEngine;

public class FSMSoliderAttack : FsmState<Solider>
{
    protected override void OnInit(IFsm<Solider> fsm)
    {
        base.OnInit(fsm);
    }

    protected override void OnEnter(IFsm<Solider> fsm)
    {
        base.OnEnter(fsm);
        fsm.Owner.transform.LookAt(fsm.Owner.TargetSolider.transform.position);
        fsm.Owner.ChangeState(Solider.State.Attack_Enemy);
    }

    protected override void OnLeave(IFsm<Solider> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
    }

    protected override void OnUpdate(IFsm<Solider> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        if (fsm.Owner.TargetSolider && fsm.Owner.TargetSolider.IsDead() == false) 
        {
            fsm.Owner.DoAttack();    
        }
    }

    protected override void OnDestroy(IFsm<Solider> fsm)
    {
        base.OnDestroy(fsm);
    }
}