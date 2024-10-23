using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class InitProperty : Action
    {
        public SharedGameObject targetObj;
        //public shared

        public override void OnBehaviorRestart()
        {
            //TestBranch
            base.OnBehaviorRestart();
<<<<<<< HEAD
            
=======
            //this.Owner. selfTrans.SetValue(this.GameObject.transform);
>>>>>>> 7398cbc59f316b1f066607f9c571d0c750119c0f
        }

        public override void OnFixedUpdate()
        {
            base.OnFixedUpdate();
        }
    }
}