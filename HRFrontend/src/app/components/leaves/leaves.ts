import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { LeaveService } from '../../services/leave';
import { AuthService } from '../../services/auth';
import { NotificationService } from '../../services/notification';
import { Router } from '@angular/router';

@Component({
  selector: 'app-leaves',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './leaves.html',
  styleUrl: './leaves.scss'
})
export class LeavesComponent implements OnInit {
  leaves: any[] = [];
  employeeNotifications: any[] = [];
  unreadCount = 0;
  showForm = false;
  role = '';

  newLeave = {
    employeeId: 0,
    employeeName: '',
    type: 'Annual',
    startDate: '',
    endDate: '',
    reason: ''
  };

  constructor(
    private leaveService: LeaveService,
    private authService: AuthService,
    private notificationService: NotificationService,
    private router: Router
  ) {}

  ngOnInit() {
    this.role = this.authService.getRole() || '';
    this.loadLeaves();

    // Si employé, charge ses notifications
    if (this.role === 'Employee') {
      const employeeId = localStorage.getItem('employeeId');
      if (employeeId) {
        this.loadEmployeeNotifications(+employeeId);
      }
    }
  }

  loadLeaves() {
    this.leaveService.getAll().subscribe({
      next: (data) => {
        if (this.role === 'Employee') {
          const employeeId = localStorage.getItem('employeeId');
          this.leaves = data.filter((l: any) => l.employeeId == employeeId);
        } else {
          this.leaves = data;
        }
      },
      error: () => this.router.navigate(['/login'])
    });
  }

  loadEmployeeNotifications(employeeId: number) {
    this.notificationService.getByEmployee(employeeId).subscribe({
      next: (data) => {
        this.unreadCount = data.count;
        this.employeeNotifications = data.notifications;
      }
    });
  }

  markAsRead(id: number) {
    this.notificationService.markAsRead(id).subscribe({
      next: () => {
        const employeeId = localStorage.getItem('employeeId');
        if (employeeId) this.loadEmployeeNotifications(+employeeId);
      }
    });
  }

  submitLeave() {
    this.leaveService.create(this.newLeave).subscribe({
      next: () => {
        this.loadLeaves();
        this.showForm = false;
      }
    });
  }

  updateStatus(id: number, status: string) {
    this.leaveService.updateStatus(id, status).subscribe({
      next: () => this.loadLeaves()
    });
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}