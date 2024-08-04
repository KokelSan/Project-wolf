using UnityEngine;
using HuggingFace.API;
using System.Collections;
using System.Text;
using UnityEngine.Networking;

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

    #region Webservice Conversation

    private const string apiKey = "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";

    public IEnumerator StartWebserviceConversation()
    {
        // Préparation des données JSON
            //  Inputs = "Hello",
            //  Parameters = new GenerateTextRequestParameters
            //  {
            //      Max_new_tokens = 2501,
            //      Return_full_text = false,
            //  },
            //  Options = new GenerateTextRequestOptions
            //  {
            //      Use_cache = true,
            //      Wait_for_model = false,
            //  },

        string jsonData = 
            "{" +
                "\"inputs\": \" What is the capital of France? \"," +
                "\"parameters\": " +
                    "{" +
                        "\"max_new_tokens\": \" 1000 \"," +
                        "\"return_full_text\": \" false \"," + 
                    "}," +
                "\"options\": " +
                    "{" +
                        "\"use_cache\": \" true \"," +
                        "\"wait_for_model\": \" false \"," +
                    "}," +
            "}";
        byte[] postData = Encoding.UTF8.GetBytes(jsonData);

        // Création de la requête
        using (UnityWebRequest request = new UnityWebRequest(ApiEndpoints.GetUrl(Model), "POST"))
        {
            request.SetRequestHeader("Authorization", "Bearer " + apiKey);
            request.SetRequestHeader("Content-Type", "application/json");            
            request.uploadHandler = new UploadHandlerRaw(postData);
            request.downloadHandler = new DownloadHandlerBuffer();

            Debug.Log($"Conversation request sent using model {Model}");
            isWaitingForResponse = true;

            // Envoi de la requête et attente de la réponse
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Erreur: {request.error}\n");
            }
            else
            {
                // Traitement de la réponse
                Debug.Log($"Réponse: {request.downloadHandler.text}\n");
            }

            request.Dispose();
        }

        isWaitingForResponse = false;
    }

    #endregion

    #region API Conversation

    public void StartApiConversation()
    {
        if (isWaitingForResponse) return;

        isWaitingForResponse = true;
        HuggingFaceAPI.Conversation(Input, OnApiConversationSuccess, OnApiConversationError);

        Debug.Log($"Conversation request sent to {Model}: {Input}");
    }

    private void OnApiConversationSuccess(string result)
    {
        Debug.Log($"Conversation Success!\n{result}\n");
        isWaitingForResponse = false;
    }

    private void OnApiConversationError(string error)
    {
        Debug.LogError($"Conversation Error!\n{error}\n");
        isWaitingForResponse = false;
    }

    #endregion

    #region API Text Generation

    public void StartApiTextGeneration()
    {
        if (isWaitingForResponse) return;

        isWaitingForResponse = true;
        HuggingFaceAPI.Conversation(Input, OnApiTextGenerationSuccess, OnApiTextGenerationError);

        Debug.Log($"Generation request sent to {Model}: {Input}");
    }

    private void OnApiTextGenerationSuccess(string result)
    {
        Debug.Log($"Conversation Success!\n{result}\n");
        isWaitingForResponse = false;
    }

    private void OnApiTextGenerationError(string error)
    {
        Debug.LogError($"Conversation Error!\n{error}\n");
        isWaitingForResponse = false;
    }

    #endregion

    #region Example 

    // From https://huggingface.co/blog/unity-api

    // Make a call to the API
    public void Query()
    {
        string inputText = "I'm on my way to the forest.";
        string[] candidates = {
        "The player is going to the city",
        "The player is going to the wilderness",
        "The player is wandering aimlessly"
    };
        HuggingFaceAPI.SentenceSimilarity(inputText, OnSuccess, OnError, candidates);
    }

    // If successful, handle the result
    void OnSuccess(float[] result)
    {
        foreach (float value in result)
        {
            Debug.Log(value);
        }
    }

    // Otherwise, handle the error
    void OnError(string error)
    {
        Debug.LogError(error);
    }

    #endregion
}
