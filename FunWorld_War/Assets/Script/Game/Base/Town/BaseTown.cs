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

        public override ObjectType ObjectType()
        {
            return global::ObjectType.Town;
        }

        //生成士兵
        protected virtual void CreateSolider()
        {
            
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

        public void Update()
        {
            List<Solider> enemySoliders;
            if (CheckHaveEnemyInView(out enemySoliders))
            {
                CreateSolider();
            }
        }

        //附近是否有把自己当作目标的部队
        private bool CheckHaveEnemyInView(out List<Solider> enemySoliders)
        {
            enemySoliders = new List<Solider>();
            RaycastHit hit = new RaycastHit();
            Collider[] hits = new Collider[]{};
            hits = Physics.OverlapSphere(this.transform.position, ViewRedius);
            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    var tempSolider = hits[i].GetComponent<Solider>(); 
                    if (tempSolider && tempSolider.campType != this.Camp() && tempSolider.GetTargetObject() == this)
                    {
                        enemySoliders.Add(tempSolider);
                    }
                }
            }
            return enemySoliders.Count > 0;
        }
    }
}