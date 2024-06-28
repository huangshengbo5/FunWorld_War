using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Profiling;
using System;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public static void GameEnter()
    {
        SceneManager.LoadScene(1);
    }
}
