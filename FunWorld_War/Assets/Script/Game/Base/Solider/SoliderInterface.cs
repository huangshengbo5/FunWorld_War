using UnityEngine;

namespace Script.Game.Base
{
    public interface SoliderInterface
    {
        //设置目标
        public void SetTarget(Transform target);
        //获取目标
        public Transform GetTarget();
        //收到伤害
        public void SufferInjure(float injure);
    }
}