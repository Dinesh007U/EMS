# EMS
The Event Management System (EMS) project, akin to Microsoft Ignite, is a comprehensive platform designed to facilitate the organization and management of virtual events across various categories, including Tech & Innovations and Industrial events. The EMS project entails the development of a robust system with distinct layers for efficient functionality and user interaction.

### 1. Functional Requirements:
- **Admin Panel**:
  - Admin authentication with default credentials stored in the database.
  - CRUD operations for event details, including addition, removal, and updates.
  - Addition of sessions to events based on categories.
  - Management of speaker details, including addition, removal, and assignment to sessions.
  - Viewing event and session details.

- **Participant/Subscriber Interface**:
  - Registration with email and password authentication via Microsoft Entra ID.
  - Exploration of sessions across different categories without login.
  - Login authentication through Microsoft Entra ID.
  - Event registration and viewing of associated event and session details post-login.

### 2. System High-Level Design:
- **Data Access Layer (DAL)**:
  - Creation of database using Entity Framework Core Code First Approach.
  - Definition of entity classes including UserInfo, EventDetails, SpeakersDetails, SessionInfo, and ParticipantEventDetails.
  - Configuration of relationships between entities using Fluent API.
  - Implementation of CRUD functionalities with the Repository pattern.

- **App Service Layer**:
  - Development of ASP.Net Core Web API for creating RESTful services.
  - Integration with DAL to consume functionalities.
  - Authentication using JWT bearer tokens.

- **App UI Layer**:
  - Utilization of ASP.Net Core MVC Application for user interface.
  - Binding of model classes to views.
  - Implementation of controllers to interact with API endpoints from the Service Layer.
  - Utilization of two different layout views for Admin and Participant.
  - Implementation of client-side and server-side validations using DataAnnotations.
  - Implementation of CRUD functionalities for EventDetails using jQuery Ajax and Bootstrap modals.

### 3. Project Structure:
The project structure consists of three main layers:
- **AppUi**: Contains the user interface components implemented in ASP.Net Core MVC.
- **ServiceLayer**: Comprises the ASP.Net Core Web API for providing RESTful services.
- **DAL**:
  - **Repository/DataAccess**: Houses the repository pattern for CRUD operations.
  - **Models**: Contains entity classes defining the database structure.

Overall, the EMS project aims to provide a seamless experience for both administrators and participants/subscribers in managing and attending virtual events across diverse categories, employing modern technologies and best practices in software development.
