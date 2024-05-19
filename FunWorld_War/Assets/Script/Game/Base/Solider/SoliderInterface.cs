using UnityEngine;

namespace Script.Game.Base
{
    public interface SoliderInterface
    {
        public void SetTarget(Transform target);
        public Transform GetTarget();
    }
}