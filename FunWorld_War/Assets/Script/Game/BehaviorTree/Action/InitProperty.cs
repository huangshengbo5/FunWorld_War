using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class InitProperty : Action
    {
        public SharedGameObject targetObj;
        //public shared

        public override void OnBehaviorRestart()
        {
            base.OnBehaviorRestart();
            //this.Owner. selfTrans.SetValue(this.GameObject.transform);
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
            
        }
    }
}