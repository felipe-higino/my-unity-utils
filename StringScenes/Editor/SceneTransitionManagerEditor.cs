using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SO_SceneController))]
public class SO_SceneControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var Target = (SO_SceneController)target;
        if (GUILayout.Button("Load Selected Scene"))
        {
            Target.LoadSelectedScene();
        }
        base.OnInspectorGUI();
    }
}