#tool "nuget:?package=xunit.runner.console"

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

Task("dotnet-restore")
    .Does(() => 
    {
        DotNetCoreRestore("../Pactify.sln");
    });

Task("dotnet-build")
    .IsDependentOn("dotnet-restore")	
    .Does(() => 
    {
        DotNetCoreBuild("../Pactify.sln", new DotNetCoreBuildSettings 
        {
            Configuration = configuration,
            MSBuildSettings = new DotNetCoreMSBuildSettings
            {
                TreatAllWarningsAs = MSBuildTreatAllWarningsAs.Error
            }
        });
    });

Task("run-xunit-tests")	
    .IsDependentOn("dotnet-build")
    .Does(() => 
    {
        var settings = new DotNetCoreTestSettings
        {
            Configuration = configuration
        };
    
        DotNetCoreTest("../tests/Pactify.UnitTests/Pactify.UnitTests.csproj", settings);
    });	

Task("Default")
    .IsDependentOn("run-xunit-tests");

RunTarget(target);