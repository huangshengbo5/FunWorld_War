using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class MoveToTarget : Action
    {
        public Transform targetTrans;
        public float speed;
        public float delay;


        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            
        }
    }
}