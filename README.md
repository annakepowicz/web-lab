# Web Applications on .NET Platform - Laboratories

This repository contains solutions for the laboratory tasks associated with the "Web Applications on .NET Platform" course at Wrocław University of Science and Technology (PWr). The project covers a progression from basic web protocols and frontend technologies to advanced C# programming and ASP.NET Core MVC development.

## 🛠 Tech Stack

* **Platform:** .NET 8.0 
* **Language:** C# 
* **Frontend:** HTML5, CSS3, JavaScript 
* **Framework:** ASP.NET Core MVC 

## 📂 Laboratory List

The following table summarizes the key topics and tasks implemented in each laboratory session:

| Lab | Topic | Description |
| :--- | :--- | :--- |
| **Lab 01** | **HTTP & HTML Basics** | Analysis of the HTTP protocol using `curl` (headers, GET/POST methods). Creating a basic HTML page without CSS/JS containing links, images, and various headers. |
| **Lab 02** | **HTML5 Advanced** | Implementation of complex HTML5 structures: nested lists, definition lists, tables with merged cells, forms with various input types, and internal linking. Usage of semantic tags and meta elements. |
| **Lab 03** | **CSS3** | Styling web pages using internal and external stylesheets. Exploring complex selectors, the box-model, positioning, and pseudo-classes. Implementation of a CSS-only dropdown menu. |
| **Lab 04** | **JavaScript** | DOM manipulation: creating a multiplication table generator with random ranges. Working with the HTML5 `<canvas>` element to draw lines reacting to mouse movement. |
| **Lab 05** | **.NET Setup & MVC** | Setup of Visual Studio 2022 Community. Creation of a console application to solve quadratic equations ($ax^2+bx+c=0$) and integrating this logic into a basic ASP.NET Core MVC project. |
| **Lab 06** | **C# Language Features** | Exploration of C# specifics: implicit typing (`var`), anonymous types, and tuples. Implementation of methods with named/optional parameters and pattern matching using `switch`. |
| **Lab 07** | **LINQ & Reflection** | Advanced data manipulation using LINQ (grouping, sorting, projecting data). Using Reflection to dynamically instantiate objects and invoke methods by name. |
| **Lab 08** | **MVC Routing** | Implementing URL-based routing logic. `ToolController` for solving equations via URL parameters and a `GameController` implementing a "Guess the Number" game with state persistence. |
| **Lab 09** | **DI & CRUD** |   Dependency Injection and Views: Creating an `Article` model and implementing an `IArticlesContext` interface with two different collection types (List vs. Dictionary). Building CRUD views and using Layout sections. |

## 🚀 How to Run

### .NET Applications (Labs 05-09)
Ensure you have the .NET SDK installed.
1.  Navigate to the project directory.
2.  Run the application:
    ```bash
    dotnet run
    ```

