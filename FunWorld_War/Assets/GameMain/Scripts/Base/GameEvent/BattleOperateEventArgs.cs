using GameFramework;
using GameFramework.Event;
using Script.Game.Base;

public class BattleClickPlayerTownEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(BattleClickPlayerTownEventArgs).GetHashCode();
    
    
    public static BattleClickPlayerTownEventArgs Create(int townId,CampType ownerType)
    {
        BattleClickPlayerTownEventArgs battleClickPlayerTown = ReferencePool.Acquire<BattleClickPlayerTownEventArgs>();
        return battleClickPlayerTown;
    }

    private BaseTown town;
    
    public BaseTown Town { get; set; }
    
    public override int Id { get {return EventId;} }
    public override void Clear() { }
}