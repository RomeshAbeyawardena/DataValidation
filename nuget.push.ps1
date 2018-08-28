$source = "https://api.nuget.org/v3/index.json"
$c = dir **\*.nupkg

Foreach ($i in $c){
    Write-Output("Pushing $i.FullName to $source")
    dotnet nuget push $i -k oy2dsdppjqotghgt52zqkaci6bw2bxrexbb3qbkhsekkuy -s "$source"
}