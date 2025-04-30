# AI Agent Project

This project implements an AI agent that interacts with the Azure AI Agent Service. It is designed to provide a framework for building and deploying AI-driven applications using Azure's capabilities.

## Project Structure

```
ai-agent-project
├── src
│   ├── main.py                # Entry point of the application
│   ├── services
│   │   └── azure_agent_service.py  # Service for Azure AI Agent interactions
│   ├── models
│   │   └── __init__.py        # Data models for the AI agent
│   └── utils
│       └── helpers.py         # Utility functions for common tasks
├── requirements.txt           # Project dependencies
├── .env                       # Environment variables for configuration
├── .gitignore                 # Files and directories to ignore by Git
└── README.md                  # Project documentation
```

## Setup Instructions

1. **Clone the repository:**
   ```
   git clone <repository-url>
   cd ai-agent-project
   ```

2. **Create a virtual environment:**
   
   python -m venv venv
   source venv/bin/activate # On Windows use `venv\Scripts\activate`
   ```

3. **Install dependencies:**
   ```
   pip install -r requirements.txt
   pip install --user -r requirements.txt ( install to user available location)
   ```

4. **Configure environment variables:**
   Create a `.env` file in the root directory and add your Azure credentials and other necessary configurations.

5. **Run the application:**
   ```
   python src/main.py
   ```

## Usage Examples

- Initialize the AI agent and connect to the Azure AI Agent Service.
- Send requests to the Azure AI Agent and receive responses.

## Contributing

Feel free to submit issues or pull requests for improvements or bug fixes.

## License

This project is licensed under the MIT License.

https://learn.microsoft.com/en-us/azure/ai-services/agents/quickstart?pivots=programming-language-python-azure









