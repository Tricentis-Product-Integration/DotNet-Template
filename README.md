# Rest API Template using ASP.NET Core


### Uses .NET 8

## Web App Included Packages
### - Swashbuckle 6.4.0
### - Microsoft.EntityFrameworkCore.InMemeory 8.0.6

<br/>

## Test Project Included Packages
### - FluentAssertions 6.12.0
### - NUnit3 Test Adapter 4.5.0
### - Moq 4.20.70
### - Microsoft.NET.Test.Sdk 17.8.0
### - NUnit 4.0.0

<br/>

## CI/CD Pipeline
### Github Workflow
 - Pull Request: Checkout, Setup .NET, and Runs tests when pull request opened to main branch.
 - Release: Checkout, Setup .NET, Builds .NET Application, and creates release asset when release is published.
 - SonarQube: Checkout, Setup .NET, Cache SonarQube packages, and Build and Analyze on Pull Request, Release, and at 4:00 AM daily.

<br/>

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
