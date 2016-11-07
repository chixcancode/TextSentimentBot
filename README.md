# TextSentimentBot
This chat bot integrates Cognitive Services Text Analytics to show sentiment score and keywords from a chat.  To use, you must create a new cognitive services in Azure Portal:

1)  Navigate to Cognitive Services in the Azure Portal and ensure Text Analytics is selected as the 'API type'.

2)  Select a plan. You may select the free tier for 5,000 transactions/month. As is a free plan, you will not be charged for using the service. You will need to login to your Azure subscription.

3)  Complete the other fields and create your account.

4)  After you sign up for Text Analytics, find your API Key. Copy the primary key, as you will need it when using the API services.

5)  In MessagesController, change apiKey with key in Step 4.

