using System.Collections.Generic;

public class Attribute 
{
    private float Hp;
    private float Hp_Percent;
    public float Hp_Cur;   //当前血量
    public float Hp_Max;

    private float Atk;
    private float Atk_Percent;
    public float Atk_Cur;  //当前攻击力

    private float Damage_Cur; //当前伤害值
    public float Speed_Cur; //当前速度

    public float Vision;  //当前视野

    private Dictionary<string, float> dic_Attr;
    
    //添加属性配置
    public void AddAttribute(string attrs)
    {
        if (dic_Attr == null)
        {
            dic_Attr = new Dictionary<string, float>();
        }
        
    }
    
}