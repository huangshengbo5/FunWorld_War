using Script.Game;
using Script.Game.Base;
using UnityEngine;
using Random = System.Random;

public class Town_City : BaseTown
{
    public GameObject ObjSolider;
    public Transform TargetActor;
    
    private void Start()
    {
        CurSoliderNum = DefaultMaxSoliderNum;
        CurSoliderNum_Txt.SetText(CurSoliderNum.ToString());
        CreateSoliders();
    }

    public void InitTown()
    {
        
    }

    protected void CreateSoliders()
    {
        for (int i = 0; i < DefaultMaxSoliderNum; i++)
        {
            var createSolider = CreateSolider(i); 
            Soliders.Add(createSolider);
            createSolider.Init();
        }
    }
    
    //创建士兵
    protected Solider CreateSolider(int index)
    {
        var solider = (GameObject)Instantiate(ObjSolider);
        solider.name = string.Format("Solider_{0}_{1}",OwnerType.ToString(),index) ;
        var soliderTans = solider.GetComponent<Transform>();
        soliderTans.position = GetSoliderPosition();
        soliderTans.localScale = Vector3.one;
        soliderTans.rotation = Quaternion.identity;
        var soliderCom = solider.GetComponent<Solider>();
        soliderCom.OwnerType = OwnerType;
        return soliderCom;
    }

    //获取士兵位置
    Vector3 GetSoliderPosition()
    {
        var selfPosition = this.gameObject.transform.position;
        var random = new Random();
        var posx = random.Next((int)selfPosition.x+3,(int)selfPosition.x+4);
        //var posy = random.Next((int)selfPosition.y,(int)selfPosition.y+10);
        var posz = random.Next((int)selfPosition.z+3,(int)selfPosition.z+4);
        return new Vector3(posx,0,posz);
    }

    private void Update()
    {
        switch (OwnerType)
        {
            case TownOwnerType.Player:
                break;
        }
    }
}