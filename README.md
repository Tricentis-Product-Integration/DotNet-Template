# Rest API Template using ASP.NET Core


## Web App Included Packages
 - Swashbuckle
 - Microsoft.EntityFrameworkCore.InMemeory



## Test Project Included Packages
 - FluentAssertions
 - NUnit3 Test Adapter
 - Moq
 - Microsoft.NET.Test.Sdk
 - NUnit



## CI/CD Pipeline
### Github Workflow
 - Pull Request: Checkout, Setup .NET, and Runs tests when pull request opened to main branch.
 - Release: Checkout, Setup .NET, Builds .NET Application, and creates release asset when release is published.


## CreateProject CLI Tool to Create Clone:
```
Used to create project using given parameters, which include name, type, version, and directory location

Usage:
  projectMaker createProject [flags]

Aliases:
Flags:
  -h, --help                     help for createProject
  -n, --name string              desired name of project
  -o, --outputDirectory string   desired output directory location of project, can be path to existing directory or can be name of a new directory to be created
  -t, --templateType string      desired type of project template, used to generate
                                 repository name: Tricentis-Product-Integration/<templateType>Template
  -v, --templateVersion string   version of template, passed as a tag to the template repository (default "latest")
```

## Example use for .NET Template
```
createProject -name "name" -templateType "dotNet" -templateVersion(optional) "1.0.0" -outputDirectory "path"
```
