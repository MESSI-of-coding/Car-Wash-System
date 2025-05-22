---
applyTo: "**"
---

# CarWash Project Coding Standards & Instructions

## Overview

This repository implements the On-Demand Car Wash System as described in the HLD document. The project follows a modular, layered architecture using ASP.NET Core (backend) and Angular (frontend). All microservice domains are implemented as modules within a single API project, with clear folder and namespace separation.

## Tech Stack

- Backend: ASP.NET Core, Entity Framework Core, ASP.NET Core Identity, JWT Authentication, Stripe (payments), RabbitMQ (mocked), Hangfire (mocked)
- Database: SQL Server (SSMS), EF Core migrations
- Frontend: Angular (TypeScript, Angular Material, RxJS)
- Testing: MSTest (unit tests for each layer)

## Architecture & Folder Structure

- **Single API Project:** All domains (User, Car, WashRequest, Payment, Notification, Review, Admin, Leaderboard) are implemented as modules within one ASP.NET Core API project.
- **Repository Pattern:** Data access is abstracted via repositories in the DAL layer.
- **Layered Structure:**
  - `CarWash.API/` (Controllers, Middleware, API setup)
  - `CarWash.BL/` (Business logic, Services, DTOs, Event mocks)
  - `CarWash.DAL/` (Repositories, EF Core DbContext, Migrations)
  - `CarWash.Domain/` (Entities/Models)
  - `CarWash.Tests/` (Unit tests for all layers)
  - `carwash-frontend/` (Angular frontend)
- **Documentation:** Maintain architecture diagrams, LLD, and instructions in the repo as you go.

## Coding Standards & Best Practices

- Use clear, consistent naming conventions (PascalCase for classes, camelCase for variables, etc.)
- Write XML comments for public classes and methods
- Use dependency injection for all services
- Implement exception handling and logging (see Middleware/ExceptionMiddleware.cs)
- Use DTOs for all API input/output
- Validate all models (data annotations and FluentValidation if needed)
- Write unit tests for each layer (BL, DAL, API)
- Use code analysis tools and follow code style guidelines
- Keep controllers thin; put business logic in services
- Use async/await for all I/O operations
- Mock RabbitMQ and Hangfire until core features are ready
- Use Swagger for API documentation and testing

## Security

- Use ASP.NET Core Identity with JWT for authentication and role-based authorization (Customer, Washer, Admin)
- Store sensitive config (e.g., Stripe keys) securely (not in source control)

## Evaluation Criteria (Checklist)

- Web API is a separate entity, follows repository pattern, and has clear folder structure
- Proper use of DTOs
- JWT-based authentication and role-based access
- Client-side controllers interact with API; validations applied
- HLD/LLD and diagrams are maintained
- Working UI with error-free validation and accurate results
- Backend scripts, queries, and solution are ready
- Code is well-structured, documented, and explainable
- Unit tests for each layer with good coverage
- Unique features are highlighted
- Understanding and effective use of libraries/frameworks
- Best practices: naming, indentation, comments, exception handling, logging, testing
- Database is normalized and well-designed
- Swagger/Postman used for API testing
- Layered architecture and proper logical flow

## How to Contribute

- Follow the folder structure and standards above
- Document any architectural or design decisions in the repo
- Ask questions if anything is unclear—do not assume
- All code must be reviewed for structure, clarity, and test coverage

---

This file is maintained by the project mentor. Update as the project evolves.
