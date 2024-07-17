using UnityEngine;

public class BaseObject : MonoBehaviour
{
    private int Id;
    public int ID 
    {
        get { return Id; }
    }

    public BaseObject()
    {
        Id = gameObject.GetHashCode();
    }
}