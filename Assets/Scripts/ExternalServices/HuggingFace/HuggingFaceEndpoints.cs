using System.Collections.Generic;

namespace ExternalServices.HuggingFace
{
    public enum Model
    {
        Phi3_mini4k_Instruct,
        Phi3_mini128k_Instruct,

        Llama3_8B_Instruct,
        Llama3_70B_Instruct,
        Llama3_405B,

        Mistral_Nemo_Instruct,
        Mistral_7B_Instruct,
        Mixtral_8x7B_Instruct,
        Mixtral_Large_Instruct,

        GPT_2,
        GPT_2_Medium,
        GPT_2_Large,
    }

    public static class HuggingFaceEndpoints
    {
        private static Dictionary<Model, string> URLs = new Dictionary<Model, string>()
        {
            [Model.Phi3_mini4k_Instruct] = "https://api-inference.huggingface.co/models/microsoft/Phi-3-mini-4k-instruct",
            [Model.Phi3_mini128k_Instruct] = "https://api-inference.huggingface.co/models/microsoft/Phi-3-mini-128k-instruct",

            [Model.Llama3_8B_Instruct] = "https://api-inference.huggingface.co/models/meta-llama/Meta-Llama-3-8B-Instruct",
            [Model.Llama3_70B_Instruct] = "https://api-inference.huggingface.co/models/meta-llama/Meta-Llama-3.1-70B-Instruct",
            [Model.Llama3_405B] = "https://api-inference.huggingface.co/models/meta-llama/Meta-Llama-3.1-405B",

            [Model.Mistral_Nemo_Instruct] = "https://api-inference.huggingface.co/models/mistralai/Mistral-Nemo-Instruct-2407",
            [Model.Mistral_7B_Instruct] = "https://api-inference.huggingface.co/models/mistralai/Mistral-7B-Instruct-v0.3",
            [Model.Mixtral_8x7B_Instruct] = "https://api-inference.huggingface.co/models/mistralai/Mixtral-8x7B-Instruct-v0.1",
            [Model.Mixtral_Large_Instruct] = "https://api-inference.huggingface.co/models/mistralai/Mistral-Large-Instruct-2407",

            [Model.GPT_2] = "https://api-inference.huggingface.co/models/openai-community/gpt2",
            [Model.GPT_2_Medium] = "https://api-inference.huggingface.co/models/openai-community/gpt2-medium",
            [Model.GPT_2_Large] = "https://api-inference.huggingface.co/models/openai-community/gpt2-large",
        };

        public static string GetUrl(Model model)
        {
            return URLs[model];
        }
    }
}