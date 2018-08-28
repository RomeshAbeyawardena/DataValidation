Class NugetSource
{
	[String]$Url
	[String]$ApiKey
}

$nugetSources = @()

$nugetSources += New-Object NugetSource -Property @{ Url = "https://api.nuget.org/v3/index.json" 
                                    ApiKey = "oy2dsdppjqotghgt52zqkaci6bw2bxrexbb3qbkhsekkuy" }
$nugetSources += New-Object NugetSource -Property @{ Url = "https://cloud.hsinet.uk/tfs/DefaultCollection/_packaging/default/nuget/v3/index.json" 
                                    ApiKey = "q2b6g56vljsgjaricumbwzbevair6ll6uxd5trl77ylil5uwboga" }

$nugetSources

$c = dir **\*.nupkg

Foreach ($i in $c){
    Foreach($nugetSource in $nugetSources){
        Write-Output("Pushing $i.FullName to $nugetSource.Url")
        dotnet nuget push $i -k $nugetSource.ApiKey -s $nugetSource.Url
    }   
}

exit 0