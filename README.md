# POS and Order Management System (Restaurant) - Backend

## Project Overview

The **POS and Order Management System** is a backend solution designed for restaurant order processing. Built with **ASP.NET Core**, **Entity Framework**, and **SQL Server**, this system ensures seamless management of menus, orders, and users. It provides different access levels for admins, cashiers, and customers, making it a comprehensive and efficient platform for restaurant management.

This project follows modern software development practices, including **SOLID principles**, **Dependency Injection**, and **Agile methodology** to ensure maintainability, scalability, and efficiency. The application is fully documented using **Swagger** and integrates **Serilog** for logging, ensuring transparency and ease of debugging.

## Technologies Used

- **Backend**:  
  - ![ASP.NET Core](https://img.shields.io/badge/ASP.NET_Core-5C2D91?style=flat&logo=dotnet&logoColor=white) **ASP.NET Core** for building the RESTful APIs.
  - ![C#](https://img.shields.io/badge/C%23-239120?style=flat&logo=csharp&logoColor=white) **C#** for server-side development.
  - ![Entity Framework](https://img.shields.io/badge/Entity_Framework-7A6F52?style=flat&logo=dotnet&logoColor=white) **Entity Framework** for data access and ORM.
  - ![SQL Server](https://img.shields.io/badge/SQL_Server-0078D4?style=flat&logo=microsoft-sql-server&logoColor=white) **SQL Server** for managing relational data.

- **Design Patterns**:  
  - ![Base Layer Pattern](https://img.shields.io/badge/Base_Layer_Pattern-1E1E1E?style=flat&logo=layer&logoColor=white) **Base Layer Pattern** for organizing project structure.
  - ![DI](https://img.shields.io/badge/Dependency_Injection-1F9C8D?style=flat&logo=dependencyinjection&logoColor=white) **Dependency Injection** for better testability and separation of concerns.
  - ![SOLID](https://img.shields.io/badge/SOLID_Principles-BC2F2F?style=flat&logo=dotnet&logoColor=white) **SOLID Principles** for creating maintainable and scalable code.

- **Tools and Services**:  
  - ![Swagger](https://img.shields.io/badge/Swagger-25A14A?style=flat&logo=swagger&logoColor=white) **Swagger** for API documentation.
  - ![Serilog](https://img.shields.io/badge/Serilog-00A1F1?style=flat&logo=serilog&logoColor=white) **Serilog** for comprehensive logging.
  - ![GitHub](https://img.shields.io/badge/GitHub-181717?style=flat&logo=github&logoColor=white) **Git** and **GitHub** for version control.

## Key Features

- **Multi-User Support**:  
  The system supports various user roles, including **Admin**, **Customer**, with different access rights and responsibilities.  
  ![Users](https://img.shields.io/badge/Multi_User_Support-FFD700?style=flat&logo=users&logoColor=black)

- **Menu Management**:  
  Easily manage and update the restaurant's menu, including creating, updating, and deleting items.  
  ![Menu](https://img.shields.io/badge/Menu_Management-FF6347?style=flat&logo=restaurant&logoColor=white)

- **Order Management**:  
  Efficiently process and track customer orders, including real-time order status updates.  
  ![Order](https://img.shields.io/badge/Order_Management-32CD32?style=flat&logo=shopping-cart&logoColor=white)

- **User Authentication**:  
  Secure user authentication and authorization, leveraging **ASP.NET Identity** for role-based access control.  
  ![Authentication](https://img.shields.io/badge/Authentication-00BFFF?style=flat&logo=key&logoColor=white)

- **Logging and Monitoring**:  
  Integrated with **Serilog** for logging and troubleshooting, ensuring transparency in system operations.  
  ![Logging](https://img.shields.io/badge/Logging-800080?style=flat&logo=logstash&logoColor=white)

## How to Run the Application

1. Clone the repository:  
   ```bash
   git clone <repository-url>
