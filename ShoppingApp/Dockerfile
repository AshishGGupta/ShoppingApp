#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#First Image used as Base
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base

#Work directory
WORKDIR /app

#Exposing ports 80 and 443 of Container for HTTP and HTTPS Requests 
EXPOSE 80
EXPOSE 443

#Image used for the Build process 
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

#Creating a work directory named src and copying .csproj files of Shopping-API and Shopping-Data projects
WORKDIR /src
COPY ["ShoppingApp/ShoppingApp.csproj", "ShoppingApp/"]
COPY ["ShoppingApp.Services/ShoppingApp.Services.csproj", "ShoppingApp.Services/"]
COPY ["ShoppingApp.DataAccess/ShoppingApp.DataAccess.csproj", "ShoppingApp.DataAccess/"]
COPY ["shoppingApp.Models/ShoppingApp.Models.csproj", "shoppingApp.Models/"]
COPY ["ShoppingApp.Common/ShoppingApp.Common.csproj", "ShoppingApp.Common/"]

#Restoring all the packages used by the project Shopping-API
RUN dotnet restore "ShoppingApp/ShoppingApp.csproj"

#Copying everything to the WORKDIR(src)
COPY . .

#New directory
WORKDIR "/src/ShoppingApp"

#Running the build command and stablishing the Compile mode and Output directory
RUN dotnet build "ShoppingApp.csproj" -o /app/build

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM build as test
WORKDIR "/src/ShoppingAppTest/"

RUN dotnet test "ShoppingAppTest.csproj" --logger "trx;LogFileName=ShoppingApp-tests.xml"

#Using the previous Build command output to make a Publish command 
FROM build AS publish

#Running the publish command and stablishing the Compile mode and Output directory 
RUN dotnet publish "ShoppingApp.csproj" -o /app/publish

#Using base image to create the final Layer
FROM base AS final

#New work directory
WORKDIR /app

#Copying everything from publish directory to /app/publish
COPY --from=publish /app/publish .

#Defining what command and value use to start the container
ENTRYPOINT ["dotnet", "ShoppingApp.dll"]