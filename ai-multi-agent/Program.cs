// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using Azure.Identity;
using Microsoft.Extensions.Configuration;
using System;

namespace Azure.AI.Projects.Tests;

public class Sample_Agent
{
    static async Task Main()
    {
        Console.WriteLine("Hello, Multi AI Agent World!");

         var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "development"; // Defaults to Production
 
         var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory) // Ensure correct path
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = "eastus2.api.azureml.ms;5c0078f8-78f1-49be-9917-bdcc35f5e831;rg-mmatonok-6104_ai;mmatonok-8994";
        string tenantId = "f3d3d42a-7c6d-4a44-aceb-ff9d7839f6d3";
        string clientId = "d877e829-4f9b-418d-b984-bff62020b4c8";
        string clientSecret = config["AzureSettings:Secret"];

        // Create credentials using ClientSecretCredential
        var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);

        AgentsClient client = new AgentsClient(connectionString, credential);

        // Step 1: Create an agent
        Response<Agent> agentResponse = await client.CreateAgentAsync(
            model: "gpt-4o-mini",
            name: "My Agent",
            instructions: "You are a helpful agent.",
            tools: new List<ToolDefinition> { new CodeInterpreterToolDefinition() });
        Agent agent = agentResponse.Value;

        // Intermission: agent should now be listed

        Response<PageableList<Agent>> agentListResponse = await client.GetAgentsAsync();

        //// Step 2: Create a thread
        Response<AgentThread> threadResponse = await client.CreateThreadAsync();
        AgentThread thread = threadResponse.Value;

        // Step 3: Add a message to a thread
        Response<ThreadMessage> messageResponse = await client.CreateMessageAsync(
            thread.Id,
            MessageRole.User,
            "I need to solve the equation `3x + 11 = 14`. Can you help me?");
        ThreadMessage message = messageResponse.Value;

        // Intermission: message is now correlated with thread
        // Intermission: listing messages will retrieve the message just added

        Response<PageableList<ThreadMessage>> messagesListResponse = await client.GetMessagesAsync(thread.Id);
        //Assert.That(messagesListResponse.Value.Data[0].Id == message.Id);

        // Step 4: Run the agent
        Response<ThreadRun> runResponse = await client.CreateRunAsync(
            thread.Id,
            agent.Id,
            additionalInstructions: "");
        ThreadRun run = runResponse.Value;

        do
        {
            await Task.Delay(TimeSpan.FromMilliseconds(500));
            runResponse = await client.GetRunAsync(thread.Id, runResponse.Value.Id);
        }
        while (runResponse.Value.Status == RunStatus.Queued
            || runResponse.Value.Status == RunStatus.InProgress);

        Response<PageableList<ThreadMessage>> afterRunMessagesResponse
            = await client.GetMessagesAsync(thread.Id);
        IReadOnlyList<ThreadMessage> messages = afterRunMessagesResponse.Value.Data;

        // Note: messages iterate from newest to oldest, with the messages[0] being the most recent
        foreach (ThreadMessage threadMessage in messages)
        {
            Console.Write($"{threadMessage.CreatedAt:yyyy-MM-dd HH:mm:ss} - {threadMessage.Role,10}: ");
            foreach (MessageContent contentItem in threadMessage.ContentItems)
            {
                if (contentItem is MessageTextContent textItem)
                {
                    Console.Write(textItem.Text);
                }
                else if (contentItem is MessageImageFileContent imageFileItem)
                {
                    Console.Write($"<image from ID: {imageFileItem.FileId}");
                }
                Console.WriteLine();
            }
        }
    }
}
