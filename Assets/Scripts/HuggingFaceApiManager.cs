using UnityEngine;
using HuggingFace.API;

public class HuggingFaceApiManager : MonoBehaviour
{
    public string Input;
    public Model Model;

    public bool IsWaitingForResponse => isWaitingForResponse; 
    private bool isWaitingForResponse = false;

    public void AbortQuery()
    {
        isWaitingForResponse = false;
    }

    #region Text Generation

    private const string API_TASK_NAME = "TextGeneration";

    public void StartGeneration()
    {
        if (isWaitingForResponse) return;

        if (!Resources.Load<APIConfig>("HuggingFaceAPIConfig").SetTaskEndpoint(API_TASK_NAME, ApiEndpoints.GetUrl(Model)))
        {
            Debug.LogError($"Task {API_TASK_NAME} not found, query aborted.");
            return;
        }

        HuggingFaceAPI.TextGeneration(Input, OnGenerationSuccess, OnGenerationError);
        isWaitingForResponse = true;

        Debug.Log($"Generation request sent. Input: {Input}");
    }

    private void OnGenerationSuccess(string result)
    {
        Debug.Log($"Generated response: {result}\n");
        isWaitingForResponse = false;
    }

    private void OnGenerationError(string error)
    {
        Debug.LogError($"An error occured: {error}\n");
        isWaitingForResponse = false;
    }

    #endregion
}
