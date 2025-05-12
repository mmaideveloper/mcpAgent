from autogen_ext.auth.azure import AzureTokenProvider
from autogen_ext.models.openai import AzureOpenAIChatCompletionClient
from autogen_core.models import UserMessage

from azure.identity import DefaultAzureCredential
import asyncio

# Create the token provider
token_provider = AzureTokenProvider(
    DefaultAzureCredential(),
    "https://cognitiveservices.azure.com/.default",
)

az_model_client = AzureOpenAIChatCompletionClient(
    azure_deployment="{your-azure-deployment}",
    model="gpt-4o",
    api_version="2024-06-01",
    azure_endpoint="https://azureopenai-bmcboot-local2.openai.azure.com/",
    azure_ad_token_provider=token_provider,  # Optional if you choose key-based authentication.
    api_key="", # For key-based authentication.
)

async def initfunction():   
   result = await az_model_client.create([UserMessage(content="What is the capital of France?", source="user")])
   print(result)
   await az_model_client.close()

   asyncio.run(initfunction)
