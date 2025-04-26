#FIST STAGE THIS IAMGE
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /sourse
#get project file and  all depandencies for this project
COPY VillaAPI/VillaAPI.csproj ./VillaAPI/
RUN dotnet restore ./VillaAPI/VillaAPI.csproj
#stage 2 copy and publish app file
COPY . . 
RUN dotnet publish ./VillaAPI/VillaAPI.csproj -c Release -o /app/publish
#multi stage create anoter image 
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT [ "dotnet","VillaAPI.dll" ]


 