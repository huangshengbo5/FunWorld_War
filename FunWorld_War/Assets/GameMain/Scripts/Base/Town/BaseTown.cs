using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Script.Game.Base
{
    public class BaseTown :BaseObject
    {
        public TextMeshProUGUI CurSoliderNum_Txt;
        
        //目标城镇
        public BaseTown TargetTown;
        
        //生成士兵的时间间隔
        public float CreateSoliderInterval;

        //默认生成士兵数量
        public int DefaultMaxSoliderNum;

        public CampType OwnerCamp = global::CampType.None;
        
        protected int CurSoliderNum;
        
        public int ViewRedius;  //视野范围
        private bool isOccupied;

        private List<Transform> SpawnerPoints;

        public bool IsOccupied
        {
            get => isOccupied;
            set => isOccupied = value;
        }
        
        public override ObjectType ObjectType()
        {
            return global::ObjectType.Town;
        }

        //生成士兵
        protected virtual SoliderCommander CreateSolider()
        {
            return null;
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

        public void Update()
        {
            List<Solider> enemySoliders;
            if (CheckHaveEnemyInView(out enemySoliders))
            {
                CreateSolider();
            }
        }

        public virtual void JoinBattle(SoliderCommander enemySoliderCommander)
        {
            
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
                    if (tempSolider && tempSolider.CampType != this.Camp() && tempSolider.GetTargetObject() == this)
                    {
                        enemySoliders.Add(tempSolider);
                    }
                }
            }
            return enemySoliders.Count > 0;
        }
        
        public override Vector3 GetInteractPoint(Vector3 position)
        {
            if (SpawnerPoints == null || SpawnerPoints.Count == 0)
            {
                if (SpawnerPoints == null)
                {
                    SpawnerPoints = new List<Transform>();
                }
                SpawnerPoints.Clear();
                var spawnerHolder = gameObject.transform.Find("Spawner").gameObject;
                var childCount = spawnerHolder.transform.childCount;
                for (int i = 0; i < childCount; i++)
                {
                    SpawnerPoints.Add(spawnerHolder.transform.GetChild(i).transform);
                }
            }
            int index = Random.Range(0, SpawnerPoints.Count-1);
            return SpawnerPoints[index].position;
        }
        
        //被点击选中
        public override void OnClick()
        {
            if (OwnerCamp == CampType.Player) //处理我方城池被选中逻辑
            {
                GameEntry.Event.Fire(this,new BattleClickPlayerTownEventArgs());
                CreateClickUI_Player();
            }
        }

        //生成被选中UI
        void CreateClickUI_Player()
        {
            
        }
    }
}