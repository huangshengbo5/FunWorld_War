using GameFramework;
using GameFramework.Event;
using Script.Game.Base;

public class BattleClickPlayerTownEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(BattleClickPlayerTownEventArgs).GetHashCode();
    
    public static BattleClickPlayerTownEventArgs Create(BaseTown town)
    {
        BattleClickPlayerTownEventArgs battleClickPlayerTown = ReferencePool.Acquire<BattleClickPlayerTownEventArgs>();
        battleClickPlayerTown.Town = town;
        return battleClickPlayerTown;
    }

    private BaseTown town;
    
    public BaseTown Town { get; set; }
    
    public override int Id { get {return EventId;} }
    public override void Clear() { }
}

public class BattleClickTargetTownEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(BattleClickTargetTownEventArgs).GetHashCode();
    
    public static BattleClickTargetTownEventArgs Create(BaseTown town)
    {
        BattleClickTargetTownEventArgs battleClickTargetTown = ReferencePool.Acquire<BattleClickTargetTownEventArgs>();
        battleClickTargetTown.Town = town;
        return battleClickTargetTown;
    }

    private BaseTown town;
    
    public BaseTown Town { get; set; }
    
    public override int Id { get {return EventId;} }
    public override void Clear() { }
}


//玩家点击位置是非UI区域
public class TouchClickNotUIEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(TouchClickNotUIEventArgs).GetHashCode();
    
    public static TouchClickNotUIEventArgs Create()
    {
        TouchClickNotUIEventArgs touchClickNotUI = ReferencePool.Acquire<TouchClickNotUIEventArgs>();
        return touchClickNotUI;
    }
    
    public override int Id { get {return EventId;} }
    public override void Clear() { }
}