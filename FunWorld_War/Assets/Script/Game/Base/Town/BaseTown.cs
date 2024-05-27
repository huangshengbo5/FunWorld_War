using TMPro;
using UnityEngine;

namespace Script.Game.Base
{
    public class BaseTown :MonoBehaviour
    {
        public TextMeshProUGUI CurSoliderNum_Txt;
        
        //生成士兵的时间间隔
        public float CreateSoliderInterval;

        //默认生成士兵数量
        public int DefaultSoliderNum;
        
        protected float LastCreateTimeStamp;

        public TownOwnerType OwnerType = TownOwnerType.None;
        
        protected int CurSoliderNum;
        
        //生成士兵
        protected BaseSolider CreateSolider()
        {
            return null;
        }
    }
}