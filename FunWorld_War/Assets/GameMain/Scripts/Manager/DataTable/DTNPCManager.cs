public class DTNPCManager : DTBaseManager<DRAttribute>
{
    public override void Initialize()
    {
        attributes = GameEntry.DataTable.GetDataTable<DRAttribute>();
    }
    
}