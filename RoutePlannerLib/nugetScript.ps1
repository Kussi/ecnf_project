Remove-Item *.nupkg
Remove-Item *.nuspec

nuget spec
nuget pack RoutePlannerLib.csproj -Symbols
nuget push kussi.RoutePlannerLib.*.nupkg 081f7be0-08df-4e8f-911b-667977496c33

Write-Host "Press any key to continue ..."
$host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
