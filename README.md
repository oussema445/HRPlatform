# 🏢 HR Platform — Microservices Architecture

> A full-stack HR Management System built with .NET 10 Microservices and Angular 19, targeting the Saudi Arabian market (Vision 2030).

![.NET](https://img.shields.io/badge/.NET-10.0-purple)
![Angular](https://img.shields.io/badge/Angular-19-red)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-blue)
![JWT](https://img.shields.io/badge/JWT-Authentication-green)

---

## 🏗️ Architecture
```
Angular Frontend (Port 4200)
           ↓
    ApiGateway — Ocelot (Port 5000)
           ↓
┌─────────────────────────────────────┐
│ AuthService        :5001            │
│ EmployeeService    :5002            │
│ LeaveService       :5003            │
│ PayrollService     :5004            │
│ DashboardService   :5005            │
│ NotificationService:5006            │
└─────────────────────────────────────┘
           ↓
      SQL Server
```

---

## 🛠️ Tech Stack

### Backend
| Technology | Usage |
|------------|-------|
| ASP.NET Core 10 | REST API Microservices |
| Entity Framework Core | ORM & Database Migrations |
| SQL Server | Relational Database |
| JWT Bearer | Authentication & Authorization |
| Ocelot | API Gateway |
| HttpClient | Inter-service Communication |

### Frontend
| Technology | Usage |
|------------|-------|
| Angular 19 | SPA Framework |
| TypeScript | Programming Language |
| SCSS | Styling |
| Angular Router | Navigation & Guards |

---

## 📦 Microservices

| Service | Port | Description |
|---------|------|-------------|
| **AuthService** | 5001 | JWT Authentication, User Management, Role-based Access |
| **EmployeeService** | 5002 | Employee CRUD, Department Management |
| **LeaveService** | 5003 | Leave Requests, Approvals, Status Tracking |
| **PayrollService** | 5004 | Salary Management, Payroll Processing |
| **DashboardService** | 5005 | Analytics, Statistics, KPIs |
| **NotificationService** | 5006 | Real-time Notifications for HR/Admin/Employee |
| **ApiGateway** | 5000 | Request Routing, CORS, Load Balancing |

---

## 🔐 Role-Based Access Control

| Feature | Admin | HR | Employee |
|---------|-------|----|----------|
| View Dashboard | ✅ | ✅ | ❌ |
| Manage Employees | ✅ | ✅ | ❌ |
| Approve Leaves | ✅ | ✅ | ❌ |
| View Payroll | ✅ | ✅ | ❌ |
| Request Leave | ✅ | ✅ | ✅ |
| View Own Leaves | ✅ | ✅ | ✅ |
| Change Password | ✅ | ✅ | ✅ |
| Receive Notifications | ✅ | ✅ | ✅ |

---

## 🚀 Getting Started

### Prerequisites
- .NET 10 SDK
- Node.js 20+
- SQL Server Express
- Angular CLI 19

### Quick Start
```bash
# Clone the repository
git clone https://github.com/oussema445/HRPlatform.git
cd HRPlatform

# Start all services (Windows)
double-click start-all.bat

# Frontend available at
http://localhost:4200
```

### Default Users

| Username | Password | Role |
|----------|----------|------|
| admin | admin123 | Admin |
| hr | hr123 | HR |
| employee | emp123 | Employee |

---

## 📱 Features

- 🔐 **JWT Authentication** with role-based access control
- 👥 **Employee Management** — CRUD with automatic account creation
- 🏖️ **Leave Management** — Request, approve, reject with notifications
- 💰 **Payroll Management** — Salary processing with monthly summaries
- 📊 **Dashboard** — Real-time statistics and KPIs
- 🔔 **Notifications** — Automatic notifications for HR/Admin/Employee
- 👤 **Employee Portal** — Personal account with leave tracking
- 🌐 **API Gateway** — Centralized routing with Ocelot

---

## 🏛️ Saudi Market Context

This project is designed with the Saudi Arabian market in mind:
- **Vision 2030** compliance for HR digitalization
- **Arabic-friendly** architecture (RTL support ready)
- **SAMA** compliance-ready architecture
- **SAR** currency support in payroll

---

## 👤 Author

**Oussema** — Full Stack Developer
- 📧 ososmhemed@gmail.com
- 🌍 Jeddah, Saudi Arabia
- 💼 GitHub: [github.com/oussema445](https://github.com/oussema445)

---

## 📄 License

MIT License — feel free to use this project as a reference.