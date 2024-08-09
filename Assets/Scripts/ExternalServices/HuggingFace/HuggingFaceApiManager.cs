using UnityEngine;
using HuggingFace.API;

namespace ExternalServices.HuggingFace
{
    public class HuggingFaceApiManager : MonoBehaviour
    {
        public string Input = "You are a creative game master for a Werevolves game session set in a western atmosphere. Imagine the names of the characters, their roles and write the first round of the game's story.";
        public Model Model;

        public bool IsWaitingForResponse { get; private set; } = false;

        public void AbortQuery()
        {
            IsWaitingForResponse = false;
        }

        private const string API_TASK_NAME = "TextGeneration";

        public void StartGeneration()
        {
            if (IsWaitingForResponse) return;

            if (!Resources.Load<APIConfig>("HuggingFaceAPIConfig").SetTaskEndpoint(API_TASK_NAME, HuggingFaceEndpoints.GetUrl(Model)))
            {
                Debug.LogError($"Task {API_TASK_NAME} not found, query aborted.");
                return;
            }

            HuggingFaceAPI.TextGeneration(Input, OnGenerationSuccess, OnGenerationError);
            IsWaitingForResponse = true;
        }

        private void OnGenerationSuccess(string result)
        {
            Debug.Log($"Generated response: {result}\n");
            IsWaitingForResponse = false;
        }

        private void OnGenerationError(string error)
        {
            Debug.LogError($"An error occured: {error}\n");
            IsWaitingForResponse = false;
        }
    }
}