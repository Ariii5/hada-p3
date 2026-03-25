# HADA P3 – Products Management Web App

## Overview
HADA P3 is a web application for managing products, built with ASP.NET Web Forms (C#).  
It provides basic CRUD operations (Create, Read, Update, Delete) and a simple navigation interface to manage product entries.

## Features
**Product Management:**  
- Add new products with code, name, amount, category, price, and creation date.  
- Update existing product information.  
- Delete products.  

**Navigation Controls:**  
- Read, Read First, Read Previous, Read Next buttons to navigate product records.  

## Technologies used
- Frontend: ASP.NET Web Forms, HTML, CSS  
- Backend: C#  
- Database: SQL Server (script included to recreate database)  

## Setup Instructions

### 1. Create Local Database
1. Open the solution in **Visual Studio 2022**.  
2. In **Solution Explorer**, right-click the `App_Data` folder → **Add → New Item → SQL Database**.  
3. Name it `Database.mdf`.

### 2. Run Database Script
1. Right-click `Database.mdf` in `App_Data` → **Open in Server Explorer → New Query**.  
2. Paste the `DatabaseScript.sql` contents (tables & default categories).  
3. Execute the script. This will create `Categories` and `Products` tables and insert default categories.

### 3. Update Connection String (if necessary)
Ensure your `Web.config` has a connection string like this:

```xml
<connectionStrings>
  <add name="ProductsDB"
       connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```
## Author
- Luis Fernando Francisco Peña – 42234381X  
- Ariadna Miralles González – 20518810G
