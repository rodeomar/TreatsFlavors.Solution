# TreatsFlavors Web Application

Welcome to the TreatsFlavors web application repository! This application allows you to manage treats and flavors.

## Table of Contents

- [Overview](#overview)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Overview

TreatsFlavors is a web application built using ASP.NET Core that allows users to manage treats and flavors. Users can register, log in, and perform CRUD (Create, Read, Update, Delete) operations on treats and flavors. Admin users have additional privileges to manage users and perform administrative tasks.

## Prerequisites

Before you begin, make sure you have the following prerequisites installed:

- [.NET SDK](https://dotnet.microsoft.com/download) (at least version 5.0)
- [Entity Framework Core CLI](https://docs.microsoft.com/en-us/ef/core/cli/dotnet)

## Getting Started

1. **Clone the repository**:

   ```bash
   git clone https://github.com/rodeomar/TreatsFlavors.Solution
   cd TreatsFlavors.Solution/TreatsFlavors
   ```

2. **Configure Database Connection**:

   Create an `appsettings.json`(Make sure to create inside the nested folder(`TreatsFlavors`) of cloned repo) file in the root directory and configure your database connection string:

   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=TreatsFlavorsDb;User=YourUserName;Password=YourPassword;"
     }
   }
   ```

3. **Apply Migrations**:

   (Before running the migrations command makue sure you have dotnet ef in your system)
   ```bash
    dotnet tool install --global dotnet-ef
   ```
   Run the following commands to apply migrations and create the database:
   
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

   Replace `InitialCreate` with an appropriate migration name.

5. Run `dotnet run` to run this project or `dotnet watch run` to run in watcher mode.

   Open a web browser and navigate to `http://localhost:5000`.

## Usage

- Register as a user and log in to access the treats and flavors management features.
- Create, read, update, and delete treats and flavors (Only read if you are not logged in).
- Add/Remove flavors to Treats.
- Add/Remove Treats to Flavors.
  

--------------
### Known Bugs 
- None

---
License Please let me know if you have any questions or concerns raed@alkhanbashi.gmail.com

Copyright (c) 2023 Raed Alkhanbashi.
