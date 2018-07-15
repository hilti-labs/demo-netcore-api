# .NET Core Web API Demo with Docker

Dockerized .NET Core Web API Demo

## Prerequisites
- .NET Core 2.x SDK: https://www.microsoft.com/net/download/macos
- Docker: https://www.docker.com/community-edition
- Optional: Docker Extension for VS Code

## Create .NET Core Web API
1. Use .NET Core CLI to create a new Web API.
    - Run `donet new` from the terminal to list installed templates.
    - Run `dotnet new webapi` to create a Web API project.
2. Add the web code generation package.
    - Run `dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design`
3. Install the AspNetCore Web API Templates
    - Run `dotnet new -i "AspNetCore.WebApi.Templates::*"`
    - Run `dotnet new webapi-templates`
4. Regenerate **ValuesController**.
    - Add the following to the csproj file:
    ```xml
    <ItemGroup>
        <DotNetCliToolReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Tools" Version="2.0.0" />
    </ItemGroup>
    ```
    - Run `dotnet restore`
    - Run `dotnet aspnet-codegenerator controller -name Values -api -actions -outDir Controllers -f`
5. Launch by running `dotnet run`
    - Open a browser to `https://localhost:5001/api/values`

## Add Docker Support
1. Add **DockerFile** file to the project root.
    ```docker
    FROM microsoft/dotnet:2.1-sdk-alpine AS build

    # Set working directory within container
    WORKDIR /app

    # Copy files
    COPY ./src .

    # Restore dependencies
    RUN dotnet restore

    # Build app
    RUN dotnet publish -c Release -o dist

    # Build runtime image
    FROM microsoft/dotnet:2.1-aspnetcore-runtime-alpine AS runtime
    COPY --from=build /app/dist .
    ENTRYPOINT ["dotnet", "demo-netcore-api.dll"]
    ```
2. Build the image
    ```
    docker build . -t architecture/demo-netcore-api
    ```
3. Run the image
    ```
    docker run -it --rm -p 8000:80 --name demo-netcore-api architecture/demo-netcore-api
    ```
    - Browse to http://localhost:8000/api/values
    - To stop and remove the container, press `Ctrl+C`

> Note: On Windows you may need to navigate to the container via IP address.  
> - See https://github.com/dotnet/dotnet-docker/blob/master/samples/aspnetapp/aspnetcore-docker-windows.md

> Note: For hosting images over HTTPS  
> - See https://github.com/dotnet/dotnet-docker/blob/master/samples/aspnetapp/aspnetcore-docker-https.md
