using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// ÓÎÏ·Èë¿Ú¡£
/// </summary>
public partial class GameEntry : MonoBehaviour
{
    private void Start()
    {
        InitBuiltinComponents();
        InitCustomComponents();
        InitCustomDebuggers();
    }
}