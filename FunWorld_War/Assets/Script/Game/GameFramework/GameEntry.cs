using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;

/// <summary>
/// ��Ϸ��ڡ�
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