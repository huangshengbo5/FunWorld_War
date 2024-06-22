using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class ConfigManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Handles.CapFunction cap = Handles.CylinderHandleCap;
        // Handles.SphereHandleCap();
        // Handles.ConeHandleCap();
        // try {
        //     OnScanStatus info = delegate (UnityEditor.Progress p) {
        //         UnityEditor.EditorUtility.DisplayProgressBar ("Scanning",p.description,p.progress);
        //     };
        //     AstarPath.active.ScanLoop (info);
			     //
        // } catch (System.Exception e) {
        //     Debug.LogError ("There was an error generating the graphs:\n"+e.ToString ()+"\n\nIf you think this is a bug, please contact me on arongranberg.com (post a comment)\n");
        //     UnityEditor.EditorUtility.DisplayDialog ("Error Generating Graphs","There was an error when generating graphs, check the console for more info","Ok");
        //     throw e;
        // } finally {
        //     UnityEditor.EditorUtility.ClearProgressBar();
        // }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
