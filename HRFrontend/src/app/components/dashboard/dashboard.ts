import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { DashboardService } from '../../services/dashboard';
import { NotificationService } from '../../services/notification';
import { AuthService } from '../../services/auth';
import { Router } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss'
})
export class DashboardComponent implements OnInit {
  summary: any = {};
  notifications: any[] = [];
  unreadCount = 0;
  fullName = '';
  role = '';

  constructor(
    private dashboardService: DashboardService,
    private notificationService: NotificationService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    this.fullName = this.authService.getFullName() || '';
    this.role = this.authService.getRole() || '';
    this.loadSummary();
    this.loadNotifications();
  }

  loadSummary() {
    this.dashboardService.getSummary().subscribe({
      next: (data) => this.summary = data,
      error: () => console.log('Erreur dashboard')
    });
  }

  loadNotifications() {
    this.notificationService.getUnread().subscribe({
      next: (data) => {
        this.unreadCount = data.count;
        this.notifications = data.notifications?.slice(0, 5) || [];
      },
      error: () => console.log('Erreur notifications')
    });
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}