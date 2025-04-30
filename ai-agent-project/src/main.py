# main.py

import os
from services.azure_agent_service import AzureAgentService

def main():
    # Load environment variables
    azure_credentials = {
        "client_id": os.getenv("AZURE_CLIENT_ID"),
        "client_secret": os.getenv("AZURE_CLIENT_SECRET"),
        "tenant_id": os.getenv("AZURE_TENANT_ID"),
        "endpoint": os.getenv("AZURE_ENDPOINT")
    }

    # Initialize the Azure AI Agent Service
    agent_service = AzureAgentService(azure_credentials)
    agent_service.initialize_agent()

    # Example of sending a request to the Azure AI Agent Service
    response = agent_service.send_request("Hello, AI!")
    print("Response from Azure AI Agent:", response)

if __name__ == "__main__":
    main()