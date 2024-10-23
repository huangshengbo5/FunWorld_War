using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script.Game.Base
{
    public class BaseTown :BaseObject
    {
        public TextMeshProUGUI CurSoliderNum_Txt;
        
        //生成士兵的时间间隔
        public float CreateSoliderInterval;

        //默认生成士兵数量
        public int DefaultMaxSoliderNum;
        
        protected float LastCreateTimeStamp;

        [FormerlySerializedAs("OwnerType")] public CampType OwnerCamp = global::CampType.None;
        
        protected int CurSoliderNum;

        protected List<Solider> Soliders = new List<Solider>();

        public int ViewRedius;  //视野范围
        //生成士兵
        protected Solider CreateSolider()
        {
            return null;
        }

        public List<Solider> GetAllSoliders()
        {
            return Soliders;
        }

        public CampType Camp()
        {
            return OwnerCamp;
        }

        public void ChangeCamp(CampType type)
        {
            OwnerCamp = type;
        }

        public virtual Tuple<bool, CampType> CheckBattleResult()
        {
            return new Tuple<bool, CampType>(false,global::CampType.None);
        }

        public virtual bool IsInBattle()
        {
            return false;
        }

        public virtual void JoinBattle(List<Solider> enemy)
        {
            
        }
    }
}