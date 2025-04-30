1. ai assisten in pyton
   azure ai agent service

   
----------------------------------------------
lsb_release -a 

install docker:
https://docs.docker.com/engine/install/ubuntu/#install-using-the-repository

--

sudo add-apt-repository ppa:dotnet/backports
sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-9.0

dotnet new webapi --no-https

--------
install local ai, dockert

https://localai.io/docs/getting-started/models/

systemctl --user start docker-desktop

systemctl --user enable docker-desktop

systemctl --user stop docker-desktop

Local AI:
desktop open ai + models

docker run -ti --name local-ai -p 8080:8080 localai/localai:latest-cpu
---

https://github.com/mmaideveloper/mcpAgent

--cline/mcp server
https://medium.com/@indirakrigan/mcp-experiment-1-using-mcp-sqllite-server-with-cline-and-azure-openai-01e760bd3651

sample: energy earch
https://github.com/Azure-Samples/azure-data-manager-for-energy-openai-demo

samle:
https://mudler.pm/posts/localai-question-answering/


--------------
Test local-ai

Azure MCP Server - ?  connect to cosmos DB? storage?


