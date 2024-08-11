using ExternalServices.HuggingFace;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HuggingFaceApiManager))]
public class HuggingFaceApiManagerCustomEditor : Editor
{
    bool editInput = true;

    public override void OnInspectorGUI()
    {
        HuggingFaceApiManager apiManager = (HuggingFaceApiManager)target;

        if (apiManager.IsWaitingForResponse)
        {
            GUILayout.Label("Waiting for response...");

            if (GUILayout.Button("Abort Query"))
            {
                apiManager.AbortQuery();
            }
            return;
        }

        EditorGUILayout.Space();
        apiManager.Model = (Model)EditorGUILayout.EnumPopup("Model", apiManager.Model);

        editInput = EditorGUILayout.Foldout(editInput, "Input");
        if (editInput)
        {
            EditorStyles.textField.wordWrap = true;
            apiManager.Input = EditorGUILayout.TextArea(apiManager.Input);
        }
        
        EditorGUILayout.Space();
        if (GUILayout.Button("Start Generation"))
        {
            apiManager.StartGeneration();
        }
    }
}