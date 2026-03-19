@echo off
echo Lancement HR Platform...

start "AuthService" cmd /k "cd /d C:\Dev\HRPlatform\AuthService && dotnet run"
timeout /t 3

start "EmployeeService" cmd /k "cd /d C:\Dev\HRPlatform\EmployeeService && dotnet run"
timeout /t 3

start "LeaveService" cmd /k "cd /d C:\Dev\HRPlatform\LeaveService && dotnet run"
timeout /t 3

start "PayrollService" cmd /k "cd /d C:\Dev\HRPlatform\PayrollService && dotnet run"
timeout /t 3

start "DashboardService" cmd /k "cd /d C:\Dev\HRPlatform\DashboardService && dotnet run"
timeout /t 3

start "NotificationService" cmd /k "cd /d C:\Dev\HRPlatform\NotificationService && dotnet run"
timeout /t 3

start "ApiGateway" cmd /k "cd /d C:\Dev\HRPlatform\ApiGateway && dotnet run"
timeout /t 3

start "Frontend" cmd /k "cd /d C:\Dev\HRPlatform\HRFrontend && ng serve"

echo Tous les services sont lances !
pause