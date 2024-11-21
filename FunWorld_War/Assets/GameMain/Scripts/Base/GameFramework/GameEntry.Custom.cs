using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using Unity.VisualScripting;
using UnityEngine;
using UnityGameFramework.Editor.ResourceTools;
using UnityGameFramework.Runtime;

/// <summary>
/// ???????
/// </summary>
public partial class GameEntry : MonoBehaviour
{
    
    public static ItemComponent Item
    {
        get;
        private set;
    }

    
    public static TouchComponent Touch { get; private set; }
    private  void InitCustomComponents()
    {
#if UNITY_EDITOR
        Touch = gameObject.GetOrAddComponent<TouchComponent_Editor>().Instance();
#else
        Touch = gameObject.GetOrAddComponent<TouchComponent_Mobile>().Instance();
#endif
    }

    private static void InitCustomDebuggers()
    {
        
    }
}