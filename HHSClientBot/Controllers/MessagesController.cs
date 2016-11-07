using Microsoft.Bot.Connector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;


namespace HHSClientBot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {

            var connector = new ConnectorClient(new Uri(activity.ServiceUrl));

            if (activity.Type == ActivityTypes.Message)
            {
                const string apiKey = "164d22cafc4d41cebe8f4f65754cba75";
                const string queryUri = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment";
                const string phraseUri = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/keyphrases";

                var client = new HttpClient
                {
                    DefaultRequestHeaders = {
                {"Ocp-Apim-Subscription-Key", apiKey},
                {"Accept", "application/json"}
            }
                };
                var sentimentInput = new BatchInput
                {
                    Documents = new List<DocumentInput> {
                new DocumentInput {
                    Id = 1,
                    Text = activity.Text,
                }
            }
                };
                var json = JsonConvert.SerializeObject(sentimentInput);

                var sentimentPost = await client.PostAsync(queryUri, new StringContent(json, Encoding.UTF8, "application/json"));
                var sentimentRawResponse = await sentimentPost.Content.ReadAsStringAsync();
                var sentimentJsonResponse = JsonConvert.DeserializeObject<BatchResult>(sentimentRawResponse);
                var sentimentScore = sentimentJsonResponse?.Documents?.FirstOrDefault()?.Score ?? 0;

             
                var keyPhrasePost = await client.PostAsync(phraseUri, new StringContent(json, Encoding.UTF8, "application/json"));
                var keyPhraseRawResponse = await keyPhrasePost.Content.ReadAsStringAsync();
                var keyPhraseJsonResponse = JsonConvert.DeserializeObject<KeyPhraseResult>(keyPhraseRawResponse);
                var keyPhrase = keyPhraseJsonResponse?.Documents?.FirstOrDefault()?.KeyPhrases ?? null;
                var keyPhraseValues = string.Join(",", keyPhrase);
                string message;
                message = $"Sentiment score is {sentimentScore}, Key Phrases are {keyPhraseValues}";
                var reply = activity.CreateReply(message);
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                //add code to handle errors, or non-messaging activities
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}