# DevFreela

## A study project for a Freelancer platform using .NET 7.0

**DevFreela** was developed to solidify concepts such as **Clean Architecture**, the **Repository Pattern**, **CQRS**, **JWT**, and **Unit Testing**. It is an API where users can register as **Freelancers** and/or **Clients**, allowing clients to post projects and freelancers to provide services.

### Main Functionality
Clients can publish projects with initial information (title, description, value), and freelancers can apply to execute these projects.

## Technologies Used ⌨️
*   ASP.NET Core 7
*   Entity Framework Core
*   PostgreSQL
*   xUnit
*   Authentication and Authorization with JWT Bearer

## Features ⚙️
*   **User Registration** (Client and Freelancer)
*   **User Login** using authentication and authorization via JWT
*   **Project CRUD:** Only the client has permission to create, edit, and delete projects
*   **Project Comments:** Clients and freelancers can leave comments for communication about the project
*   **Project Status:** The client can start (Start) and finish (Finish) the project

## Patterns, Concepts, and Architecture Used 📂
*   Repository Pattern
*   Clean Architecture
*   CQRS
*   Fluent Validation for API validation
*   Unit tests with xUnit and NSubstitute

