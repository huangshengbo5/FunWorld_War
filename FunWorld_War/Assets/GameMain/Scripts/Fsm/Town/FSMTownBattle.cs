using GameFramework.Fsm;
using Script.Game.Base;
using UnityEngine;

public class FSMTownBattle : FsmState<BaseTown>
{
    protected override void OnInit(IFsm<BaseTown> fsm)
    {
        base.OnInit(fsm);
    }

    protected override void OnEnter(IFsm<BaseTown> fsm)
    {
        base.OnEnter(fsm);
        //fsm.Owner.JoinBattle(fsm.Owner.GetAllSoliders());   //??????????
    }

    protected override void OnUpdate(IFsm<BaseTown> fsm, float elapseSeconds, float realElapseSeconds)
    {
        base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
        var result = fsm.Owner.CheckBattleResult();
        if (result.Item1)
        {
            ChangeState<FSMTownBattleEnd>(fsm);
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