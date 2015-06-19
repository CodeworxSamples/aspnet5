# What's New for Web Developer in Visual Studio 2015

## Introduction

We, Raphael Schwarz from [codeworx](http://www.codeworx.at) and Rainer Stropek 
from [time cockpit](http://www.timecockpit.com), did a session at the *//build debriefing* event
from Microsoft Austria in June 2015. In this session we provided a brief overview about
what changes in *Visual Studio 2015* for web development. Raphael concentrated on the server-side,
Rainer covered client-side web development.

Here are the topics we talked about during the session:

* [ASP.NET 5](http://docs.asp.net/en/latest/) pipeline
    * Static content
    * Web API
* [Entity Framework](https://github.com/aspnet/EntityFramework) 7
    * Using EF together with ASP.NET 5 (ASP.NET dependency injection)
    * EF migrations with [DNX](http://docs.asp.net/en/latest/dnx/index.html) tool
* REST web services metadata and documentation with [Swagger](http://swagger.io/)
* Client-side dependency management
    * [NodeJS](https://nodejs.org/) and [NPM](https://www.npmjs.com/) for development dependencies
    * [Bower](http://bower.io/) for client-side components like AngularJS or Bootstrap
    * [TSD](http://definitelytyped.org/tsd/) for TypeScript Definitions
* Client-side build automation with [Gulp](http://gulpjs.com/)
    * Gulp integration in Visual Studio 2015
* Client-side development tools and libraries
    * [TypeScript](http://www.typescriptlang.org/)
    * [SASS](http://sass-lang.com/)
    * [AngularJS](https://angularjs.org/)
* Running ASP.NET 5 on Linux

This document should walk you through the code in this repository and make it easier to find the
content you are looking for.

## Configuring ASP.NET 5 Pipeline

[Startup.cs](VNextDemo/src/VNextDemo/Startup.cs) contains the code that configures the ASP.NET pipeline.
It configures *Entity Framework* and *Swagger* with just a few lines of code.

## Entity Framework 7

Model and context for this sample are quite simple. You can find the model in 
[Book.cs](VNextDemo/Model/Book.cs). The context is in [BookContext](VNextDemo/BookContext.cs).

Once you created context and model, you can use the *dnx* tool to work with EF Migrations. Here are
some examples:

````
# Create a migration
dnx . ef migration add InitialCreate

# Create migration SQL script
dnx . ef migration script

# Apply migration to database
dnx . ef migration apply
````

Why this change from Powershell to *dnx*? The reason is the support of Linux in addition to Windows.
*dnx* works on Linux, too.

## Client-side dependency management

The primary focus of the client-side part of the session is introducing web development tools that
Microsoft-oriented developers might not already use.

* In [package.json](VNextDemo/package.json) you find development dependencies. This is necessary
as other tools like *Gulp* are based on *Node.js*.
* [bower.json](VNextDemo/bower.json) references client-side libraries like *Bootstrap* or *AngularJS*.
* [tsd.json](VNextDemo/tsd.json) contains references to TypeScript definitions.
* Finally, [Gulpfile.js](VNextDemo/Gulpfile.js) contains the build automation code.

During the session we showed the corresponding tools in the command line on Windows and Linux as well
as in Visual Studio's new *Task Runner Explorer*.

## Client-side web development

The folder [wwwroot](VNextDemo/wwwroot) contains the client-side implementation using SASS, 
AngularJS, and TypeScript.