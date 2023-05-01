namespace VietJob_V2.Models.Interfaces
{
    public interface ChatGPTInterface
    {
        public string SendRequest(string request);

        public string GetResponse(string response);
    }
}
