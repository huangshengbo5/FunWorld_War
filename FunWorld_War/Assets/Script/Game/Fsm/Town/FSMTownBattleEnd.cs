using GameFramework.Fsm;
using Script.Game.Base;
using UnityEngine;

public class FSMTownBattleEnd : FsmState<BaseTown>
{
    protected override void OnInit(IFsm<BaseTown> fsm)
    {
        base.OnInit(fsm);
    }

    protected override void OnEnter(IFsm<BaseTown> fsm)
    {
        base.OnEnter(fsm);
        var result = fsm.Owner.CheckBattleResult();
        var townOwnerType = result.Item2;
        fsm.Owner.ChangeCamp(townOwnerType);
    }

    protected override void OnUpdate(IFsm<BaseTown> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
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