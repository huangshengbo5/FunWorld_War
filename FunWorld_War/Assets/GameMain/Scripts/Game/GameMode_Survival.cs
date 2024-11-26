using System;
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
}