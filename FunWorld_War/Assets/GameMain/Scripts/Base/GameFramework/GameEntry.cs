using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityGameFramework.Runtime;

public partial class GameEntry : MonoBehaviour
{
    
    
    private void Start()
    {
        InitBuiltinComponents();
        InitCustomComponents();
        InitCustomDebuggers();
        //Main.GameEnter();
        DontDestroyOnLoad(this.gameObject);
    }
}