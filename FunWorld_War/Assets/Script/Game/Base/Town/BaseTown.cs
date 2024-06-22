using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Game.Base
{
    public class BaseTown :MonoBehaviour
    {
        public TextMeshProUGUI CurSoliderNum_Txt;
        
        //生成士兵的时间间隔
        public float CreateSoliderInterval;

        //默认生成士兵数量
        public int DefaultMaxSoliderNum;
        
        protected float LastCreateTimeStamp;

        public TownOwnerType OwnerType = TownOwnerType.None;
        
        protected int CurSoliderNum;

        protected List<BaseSolider> Soliders = new List<BaseSolider>();
        //生成士兵
        protected BaseSolider CreateSolider()
        {
            return null;
        }

        public List<BaseSolider> GetAllSoliders()
        {
            return Soliders;
        }

        public TownOwnerType TownType()
        {
            return OwnerType;
        }
        
    }
}