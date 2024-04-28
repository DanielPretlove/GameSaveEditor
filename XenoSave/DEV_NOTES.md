# Building
First set up a Local NuGet Package Repo on your PC, otherwise the Build/Pack tasks will fail when trying to push the created NuGet Package to the local NuGet Package repo (which will not exist if you do not set it up first).

Then, use default "dotnet build" command to build+pack+push NuGet Package of dmm.XenoSave to a Local NuGet Package Repo on your PC for your projects to use as a dependency.

# Creating/Using Local Nuget Package
## Creating Local NuGet Package Repo on your PC
Create a folder somewhere on your PC for your .NET NuGet install to store local NuGet packages.

Then create a local NuGet Package Repo using "dotnet nuget add source <full path to your folder> --name local"

## Packing dmm.XenoSave into Local NuGet Package
First make sure that you have a Local NuGet Cache setup as per the previous section.

Then, build dmm.XenoSave as normal. The .csproj is configured to:
1. Build dmm.XenoSave
2. Delete any old versions of dmm.XenoSave from your NuGet cache
3. Pack dmm.XenoSave into a NuGet package
4. Push the packed .nupkg to your local NuGet Repo on your PC

## Using Local NuGet Package as dependency in other projects (e.g. XenoSaveCheat)
Now that you can build and push a dmm.XenoSave Nuget Package to a local NuGet repo on your PC, just add a new NuGet Package Reference to dmm.XenoSave to your other project's .csproj as you normally would, and it should find the dmm.XenoSave package in your local NuGet cache.

e.g. For XenoSaveCheat this can be done by going to XenoSaveCheat code folder and running command "dotnet add package dmm.XenoSave".

Once this is setup, running "dotnet restore" or "dotnet build" on your project will grab dmm.XenoSave from your local NuGet repo and copy it in your local NuGet cache so your project can use it.