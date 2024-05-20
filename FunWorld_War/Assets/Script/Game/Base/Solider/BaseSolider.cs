using System.Collections;
using System.Collections.Generic;
using Script.Game.Base;
using UnityEngine;

public class BaseSolider : MonoBehaviour,SoliderInterface
{

    private Transform target;
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public Transform GetTarget()
    {
        return this.target;
    }

    public void SufferInjure(float injure)
    {
        throw new System.NotImplementedException();
    }
}
