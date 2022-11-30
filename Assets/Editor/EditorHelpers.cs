using System;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
[CanEditMultipleObjects]
public class EditorHelpers : Editor {

    static EditorHelpers() {
        EditorApplication.update += Update;
    }
    private static void Update() {
        var vec = new Vector3(90, 0, 0);
        foreach (var o in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects()) {
            if (o.CompareTag("MainCamera")) {
                continue;
            }
            o.transform.rotation = Quaternion.Euler(vec);
        }
    }
}
