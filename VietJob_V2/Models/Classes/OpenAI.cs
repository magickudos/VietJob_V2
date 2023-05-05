using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenAI_API;

namespace VietJob_V2.Models.Classes
{
    public class OpenAi
    {
        private readonly OpenAIAPI _api = new OpenAIAPI("YOUR_API_KEY");

        public async Task<string> MakeConversation(string question)
        {
            var chat = _api.Chat.CreateConversation();
            
            chat.AppendUserInput(question);
            var response = await chat.GetResponseFromChatbotAsync();
            return response;
        }
    }
}
