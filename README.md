<link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.8.1/css/all.css">
<link rel="stylesheet" href="assets/styles/homepage.css">
<div class="banner">
    <div class="banner-content">
        <div class="container-fluid">
            <div class="row">
                <div class="banner-img col-md-3 col-md-offset-1" align="center">
                    <img alt="RailSharp" src="https://imgur.com/h5hgvh7.png" height="200px" class="img-responsive">
                </div>
                <div class="banner-txt col-md-7">
                    <h1 align="center">RailSharp</h1>
                    <p align="center">
                        A small railway oriented library that offers a simple implementation of the
                        <a href="http://codinghelmet.com/articles/understanding-the-option-maybe-functional-type">Option type</a>
                        and Result type in C#.
                    </p>
                    <p align="center">
                        <a href="https://dev.azure.com/SoftFrame/RailSharp/_release?definitionId=2&_a=deployments">
                            <img alt="Azure DevOps releases" src="https://img.shields.io/azure-devops/release/softframe/c8394a74-6f1e-441d-8ef1-8a1845f52445/2/5.svg?style=flat-square">
                        </a>
                        <a href="https://dev.azure.com/SoftFrame/RailSharp/_build/latest?definitionId=2&branchName=develop">
                            <img alt="Azure DevOps builds" src="https://img.shields.io/azure-devops/build/softframe/c8394a74-6f1e-441d-8ef1-8a1845f52445/2/develop.svg?style=flat-square">
                        </a>
                        <a href="https://dev.azure.com/SoftFrame/RailSharp/_build/latest?definitionId=2&branchName=develop">
                            <img alt="Azure DevOps tests" src="https://img.shields.io/azure-devops/tests/softframe/railsharp/2/develop.svg?style=flat-square">
                        </a>
                        <a href="https://dev.azure.com/SoftFrame/RailSharp/_build/latest?definitionId=2&branchName=develop">
                            <img alt="Azure DevOps coverage" src="https://img.shields.io/azure-devops/coverage/softframe/railsharp/2/develop.svg?style=flat-square">
                        </a>
                        <a href="https://www.nuget.org/packages/RailSharp">
                            <img alt="Nuget" src="https://img.shields.io/nuget/v/railsharp.svg?style=flat-square">
                        </a>
                        <a href="https://github.com/softframe/railsharp/blob/master/LICENSE">
                            <img alt="License" src="https://img.shields.io/github/license/softframe/railsharp.svg?style=flat-square">
                        </a>
                    </p>
                    <!-- <p align="center">
                        <b>Quick links:</b>
                        <span><a href="https://softframe.github.io/railsharp/articles/getting-started.html">Getting Started</a>,</span>
                        <span class="hidden"><a href="https://softframe.github.io/railsharp">Docs</a>,</span>
                        <span><a href="https://softframe.github.io/railsharp/api/railsharp.html">API</a></span>
                    </p> -->
                </div>
            </div>
        </div>
    </div>
    <a href="https://github.com/softframe/railsharp" target="_blank" class="banner-btn btn btn-primary"><i></i><span></span></a>
</div>

# What is RailSharp?

RailSharp uses railway oriented programming to help handle errors and to deal with our old friend `NullReferenceException`.

To learn more about railway oriented programming, take a look [here](https://fsharpforfunandprofit.com/rop/)!

<!-- # How do I get started? -->

# How do I install it?

First, [install NuGet][nuget-install-url].  Then, install the NuGet package avalaible at [nuget.org][nuget-pkg-url] using the dotnet CLI:

```bash
dotnet add package RailSharp
```

# I have an issue...

First, you can check if your issue has already been tracked [here][issues-url].

Otherwise, you can check if it's already fixed by pulling the [develop branch][develop-branch-url], building the solution and then using the generated DLL files direcly in your project.

If you still hit a problem, please document it and post it [here][new-issue-url].

# Credits

The original code has been inspired from the Pluralsight course [Making Your C# Code More Functional](https://www.pluralsight.com/courses/making-functional-csharp) by [Zoran Horvat](https://www.pluralsight.com/authors/zoran-horvat).

# License

RailSharp is Copyright © 2018 SoftFrame under the [MIT license][license-url].

<!-- Resources: -->
[develop-branch-url]: https://github.com/softframe/railsharp/tree/develop
[issues-url]: https://github.com/softframe/solidstack/issues
[license-url]: https://github.com/softframe/railsharp/blob/master/LICENSE
[new-issue-url]: https://github.com/softframe/solidstack/issues/new
[nuget-pkg-url]: https://www.nuget.org/packages/RailSharp
[nuget-install-url]: http://docs.nuget.org/docs/start-here/installing-nuget