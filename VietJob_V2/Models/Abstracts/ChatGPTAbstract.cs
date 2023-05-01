using System;
using VietJob_V2.Models.Classes;
using VietJob_V2.Models.Interfaces;

namespace VietJob_V2.Models.Abstracts
{
    public class ChatGPTAbstract : ChatGPTInterface
    {
        string request = ""; 

        #region private methods
        string? ChatGPTInterface.SendRequest(string request)
        {
            return null;
        }

        string ChatGPTInterface.GetResponse(string response)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region public methods
        public void setRequest(string requested)
        {
            request = requested;
        }

        public string getResponse ()
        {
            return null;
        }
        #endregion
    }
}
