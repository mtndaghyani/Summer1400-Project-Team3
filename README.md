# Summer1400-Project-Team3
> The final project of the [Star-Academy](https://code-star.ir/) software engineering internship in Summer 2021.

## Overview
In this projects, my teammates and I worked on a data ETL tool, implemented in C#. It can be used to extract data from CSV files and SQL Server tables, change and clean them using variant transformations and finally load them to new destinations.

## Technologies

+ Microsoft .Net 5
+ Microsoft ASP.net web api
+ MS SQL Server
+ Entitiy Framework Core
+ [ETLBox](https://www.etlbox.net/)

## Launch
To enjoy using the ETL tool RESTful API:
1. Make sure that you have installed the [dotnet ef](https://docs.microsoft.com/en-us/ef/core/cli/dotnet) CLI tool.
2. In the `./ETLLibrary` directory, enter the below command to apply migrations and create the database:
```
dotnet ef database update
```
3. Next, run the ETLWebapp using `dotnet run`
4. Finally, go to `https://localhost:5001/swagger/index.html` to see the api documentation. 

## Front-end
You can take a look at the front-end project implemented by the front-end team [here](https://github.com/mtndaghyani/Summer1400-Project-Team3/tree/frontend).
