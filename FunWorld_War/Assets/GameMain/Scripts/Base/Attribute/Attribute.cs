using System.Collections.Generic;
using Unity.VisualScripting;

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
    public bool AddAttribute(string attrs)
    {
        if (attrs.Equals(string.Empty))
        {
            return false;
        }
        if (dic_Attr == null)
        {
            dic_Attr = new Dictionary<string, float>();
        }
        attrs.Trim();
        attrs.TrimStart();
        attrs.TrimEnd();
        var attrsStr = attrs.Split(';');
        for (int i = 0; i < attrsStr.Length; i++)
        {
            var singleAttr = attrsStr[i];
            var singleAttrStr = singleAttr.Split(',');
            if (singleAttrStr.Length < 2)
            {
                return false;
            }
            string AttrKey = singleAttrStr[0];
            float AttrValue = 0f;
            if (!float.TryParse(singleAttrStr[1],out AttrValue))
            {
                return false;
            }
            if (!dic_Attr.ContainsKey(AttrKey))
            {
                dic_Attr[AttrKey] = 0;
            }
            dic_Attr[AttrKey] += AttrValue;
        }
        return true;
    }
    
    
}