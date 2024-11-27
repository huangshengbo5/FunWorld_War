using System;
using System.Collections.Generic;
using Script.Game.Base;

public class GameMode_Survival : GameBase
{
    private float m_ElapseSeconds = 0f;
    public override GameMode GameMode => GameMode.Survival;
    
    public BaseTown CurOperateTown;

    public override void Initialize()
    {
        base.Initialize();
        GameEntry.Event.Subscribe(BattleClickTargetTownEventArgs.EventId,HandlerBattleClickTargetTown);
        GameEntry.Event.Subscribe(BattleClickPlayerTownEventArgs.EventId,HandlerBattleClickPlayerTown);
        GameEntry.Event.Fire(this,GameStartEventArgs.Create());
    }

    public override void Shutdown()
    {
        base.Shutdown();
        GameEntry.Event.Unsubscribe(BattleClickTargetTownEventArgs.EventId,HandlerBattleClickTargetTown);
    }

    public void HandlerBattleClickPlayerTown(object obj, EventArgs e)
    {
        var clickEvent = e as BattleClickPlayerTownEventArgs;
        CurOperateTown = clickEvent.Town;
    }
            
    public void HandlerBattleClickTargetTown(object s ,EventArgs e)
    {
        BattleClickTargetTownEventArgs clickEventArgs = e as BattleClickTargetTownEventArgs;
        if (clickEventArgs != null)
        {
            var TargetTown = clickEventArgs.Town;
            var town = CurOperateTown as Town;
            town.AttackTargetTown(TargetTown);
        }
    }

    public override void Update(float elapseSeconds, float realElapseSeconds)
    {
        base.Update(elapseSeconds, realElapseSeconds);

        m_ElapseSeconds += elapseSeconds;
        if (m_ElapseSeconds >= 1f) m_ElapseSeconds = 0f;
    }

    /// <summary>
    /// 当前所有参与战斗的城池
    /// </summary>
    public List<BaseTown> AllBattleTowns;
    //todo 后面应该使用GameState来处理所有战场数据
    /// <summary>
    /// 城池加入战场
    /// </summary>
    /// <param name="town"></param>
    public void JoinBattle(BaseTown town)
    {
        if (AllBattleTowns == null)
        {
            AllBattleTowns = new List<BaseTown>();    
        }
        AllBattleTowns.Add(town);
    }

    public List<BaseTown> GetHostileTown(BaseTown town)
    {
        List<BaseTown> hostileTowns = new List<BaseTown>();
        foreach (var townItem in AllBattleTowns)
        {
            if (Common.GetRelation(townItem.Camp(),town.Camp()) == RelationType.Hostile)
            {
                hostileTowns.Add(townItem);
            }
        }
        return hostileTowns;
    }
    
}