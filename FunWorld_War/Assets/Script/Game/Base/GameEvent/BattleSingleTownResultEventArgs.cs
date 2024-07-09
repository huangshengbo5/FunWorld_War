using GameFramework;
using GameFramework.Event;

public class BattleSingleTownResultEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(BattleSingleTownResultEventArgs).GetHashCode();
    
    public int TownId
    {
        get;
        private set;
    }

    public TownOwnerType OwnerType
    {
        get;
        private set;
    }
    
    public static BattleSingleTownResultEventArgs Create(int townId,TownOwnerType ownerType)
    {
        BattleSingleTownResultEventArgs battleSingleTownResultEventArgs = ReferencePool.Acquire<BattleSingleTownResultEventArgs>();
        battleSingleTownResultEventArgs.TownId = townId;
        battleSingleTownResultEventArgs.OwnerType = ownerType;
        return battleSingleTownResultEventArgs;
    }
    
    public override int Id {
        get
        {
            return EventId;    
        } 
    }
    
    public override void Clear()
    {
        
    }
}