using UnityEngine;

public class BaseObject : MonoBehaviour
{
    //血量
    public int Hp;
    protected int MaxHp;
    private int Id;
    public int ID 
    {
        get { return Id; }
    }

    public BaseObject()
    {
        //Id = gameObject.GetHashCode();
    }
    
    public virtual Vector3 GetInteractPoint(Vector3 position = new Vector3())
    {
        return transform.position;
    }
    
    public virtual ObjectType ObjectType()
    {
        return global::ObjectType.None;
    }
    
    public virtual void BeAttack(BaseObject attacker, int damageNum)
    {
           
    }
}