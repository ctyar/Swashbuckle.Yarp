Get-ChildItem -Path '.\artifacts' | Remove-Item -Force -Recurse

dotnet pack src\Swashbuckle.Yarp\Swashbuckle.Yarp.csproj -o artifacts