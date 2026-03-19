import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login';
import { DashboardComponent } from './components/dashboard/dashboard';
import { EmployeesComponent } from './components/employees/employees';
import { LeavesComponent } from './components/leaves/leaves';
import { PayrollComponent } from './components/payroll/payroll';
import { NotificationsComponent } from './components/notifications/notifications';
import { authGuard } from './guards/auth-guard';
import { ProfileComponent } from './components/profile/profile';


export const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },

  {
    path: '',
    canActivate: [authGuard],
    children: [
      { path: 'dashboard', component: DashboardComponent },
      { path: 'employees', component: EmployeesComponent },
      { path: 'leaves', component: LeavesComponent },
      { path: 'payroll', component: PayrollComponent },
      { path: 'notifications', component: NotificationsComponent },
      { path: 'profile', component: ProfileComponent },
    ]
  }
];