# 🏢 HR Platform — Microservices Architecture

A full-stack HR Management System built with .NET Microservices and Angular.

## 🏗️ Architecture

| Service | Port | Description |
|---------|------|-------------|
| AuthService | 5001 | JWT Authentication & Authorization |
| EmployeeService | 5002 | Employee Management |
| LeaveService | 5003 | Leave Requests & Approvals |
| PayrollService | 5004 | Payroll & Salary Management |
| DashboardService | 5005 | Statistics & Analytics |
| NotificationService | 5006 | Real-time Notifications |
| ApiGateway | 5000 | Ocelot API Gateway |

## 🛠️ Tech Stack

- **Backend**: ASP.NET Core 10, Entity Framework Core
- **Database**: SQL Server
- **Authentication**: JWT Bearer Tokens with Roles
- **Gateway**: Ocelot API Gateway
- **Frontend**: Angular 19
- **Deploy**: Azure Container Apps

## 🚀 Getting Started
```bash
# Clone the repo
git clone https://github.com/oussema445/HRPlatform.git

# Run each service
cd AuthService && dotnet run
cd EmployeeService && dotnet run
cd LeaveService && dotnet run
cd PayrollService && dotnet run
cd DashboardService && dotnet run
cd NotificationService && dotnet run
cd ApiGateway && dotnet run
```

## 👤 Author
Oussema — Full Stack Developer