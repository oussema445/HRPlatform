@echo off
echo 🚀 Lancement HR Platform...

start "AuthService" cmd /k "cd C:\Dev\HRPlatform\AuthService && dotnet run"
timeout /t 3

start "EmployeeService" cmd /k "cd C:\Dev\HRPlatform\EmployeeService && dotnet run"
timeout /t 3

start "LeaveService" cmd /k "cd C:\Dev\HRPlatform\LeaveService && dotnet run"
timeout /t 3

start "PayrollService" cmd /k "cd C:\Dev\HRPlatform\PayrollService && dotnet run"
timeout /t 3

start "DashboardService" cmd /k "cd C:\Dev\HRPlatform\DashboardService && dotnet run"
timeout /t 3

start "NotificationService" cmd /k "cd C:\Dev\HRPlatform\NotificationService && dotnet run"
timeout /t 3

start "ApiGateway" cmd /k "cd C:\Dev\HRPlatform\ApiGateway && dotnet run"
timeout /t 3

start "Frontend" cmd /k "cd C:\Dev\HRPlatform\HRFrontend && ng serve"

echo ✅ Tous les services sont lancés !
echo Frontend: http://localhost:4200
echo Gateway:  http://localhost:5000
pause