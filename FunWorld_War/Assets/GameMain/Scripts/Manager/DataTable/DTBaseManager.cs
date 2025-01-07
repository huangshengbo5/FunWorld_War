using GameFramework.DataTable;
using UnityGameFramework.Runtime;

public abstract class DTBaseManager<T> where T : DataRowBase 
{
    protected IDataTable<T> attributes;
    
    public virtual void Initialize()
    {
        
    }
}