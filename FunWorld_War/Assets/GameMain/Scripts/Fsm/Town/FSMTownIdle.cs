using GameFramework.Fsm;
using Script.Game.Base;
using UnityEngine;

public class FSMTownIdle : FsmState<BaseTown>
{
    protected override void OnInit(IFsm<BaseTown> fsm)
    {
        base.OnInit(fsm);
    }

    protected override void OnEnter(IFsm<BaseTown> fsm)
    {
        base.OnEnter(fsm);
    }

    protected override void OnUpdate(IFsm<BaseTown> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        if (fsm.Owner.IsInBattle())
        {
            ChangeState<FSMTownBattle>(fsm);
        }
    }

    protected override void OnLeave(IFsm<BaseTown> fsm, bool isShutdown)
    {
        base.OnLeave(fsm, isShutdown);
    }

    protected override void OnDestroy(IFsm<BaseTown> fsm)
    {
        base.OnDestroy(fsm);
    }
}