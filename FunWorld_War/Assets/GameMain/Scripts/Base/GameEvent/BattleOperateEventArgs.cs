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