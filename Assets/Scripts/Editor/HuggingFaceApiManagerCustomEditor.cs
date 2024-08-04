using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HuggingFaceApiManager))]
public class HuggingFaceApiManagerCustomEditor : Editor
{
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

        GUILayout.Label("Configuration", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal();
        GUILayout.Label("Input");
        apiManager.Input = GUILayout.TextArea(apiManager.Input);
        GUILayout.EndHorizontal();

        apiManager.Model = (Model)EditorGUILayout.EnumPopup("Model", apiManager.Model);

        GUILayout.Label("");
        GUILayout.Label("Hugging Face", EditorStyles.boldLabel);        

        if (GUILayout.Button("Start API Conversation"))
        {
            apiManager.StartApiConversation();
        }

        if (GUILayout.Button("Start API Text Generation"))
        {
            apiManager.StartApiTextGeneration();
        }

        if (GUILayout.Button("Start API Example"))
        {
            apiManager.Query();
        }

        if (GUILayout.Button("Start Webservice Conversation"))
        {
            apiManager.StartCoroutine(apiManager.StartWebserviceConversation());
        }
    }
}