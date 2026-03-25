HADA P3 – Products Management Web App
Overview

HADA P3 is a web application for managing products, built with ASP.NET Web Forms (C#).
It provides basic CRUD operations (Create, Read, Update, Delete) and a simple navigation interface to manage product entries.

Features
  Product Management:
    Add new products with code, name, amount, category, price, and creation date.
    Update existing product information.
    Delete products.
  Navigation Controls:
    Read, Read First, Read Previous, Read Next buttons to navigate product records.

Technologies used:
  Frontend: ASP.NET Web Forms, HTML, CSS
  Backend: C# 
  Database: SQL Server (script included to recreate database)

Setup Instructions
  Database:
    Run DatabaseScript.sql in your SQL Server to create the database and tables (If you're not using Database.mdf inside of proWeb, remember to modify the connection string in Web.config).
  Open Project:
    Open the solution in Visual Studio 2022.
  Run Project:
    Press F5 to launch in IIS Express.
  Use the App:
    Fill out the product form and use the buttons to manage product entries.

Author
  Luis Fernando Francisco Peña – 42234381X
  Ariadna Miralles González – 20518810G
