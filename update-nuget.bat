dotnet nuget push CoubSharper.<version>.0.0.nupkg -k <key> -s https://api.nuget.org/v3/index.json
rem for %%x in (*.nupkg) do dotnet nuget push "%%x" %1 -Source https://nuget.org
