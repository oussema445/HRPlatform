import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth';
import { Router } from '@angular/router';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../../environments/environment.development';

@Component({
  selector: 'app-payroll',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './payroll.html',
  styleUrl: './payroll.scss'
})
export class PayrollComponent implements OnInit {
  payrolls: any[] = [];
  summary: any = {};
  showForm = false;
  role = '';

  newPayroll = {
    employeeId: 0,
    employeeName: '',
    month: new Date().getMonth() + 1,
    year: new Date().getFullYear(),
    basicSalary: 0,
    housingAllowance: 0,
    transportAllowance: 0,
    bonus: 0,
    deductions: 0
  };

  constructor(
    private authService: AuthService,
    private router: Router,
    private http: HttpClient
  ) {}

  private headers() {
    return new HttpHeaders({
      'Authorization': `Bearer ${this.authService.getToken()}`
    });
  }

  ngOnInit() {
    this.role = this.authService.getRole() || '';
    this.loadPayrolls();
    this.loadSummary();
  }

  loadPayrolls() {
    this.http.get<any[]>(`${environment.apiUrl}/api/payroll`, {
      headers: this.headers()
    }).subscribe({
      next: (data) => this.payrolls = data,
      error: () => this.router.navigate(['/login'])
    });
  }

  loadSummary() {
    const month = new Date().getMonth() + 1;
    const year = new Date().getFullYear();
    this.http.get<any>(`${environment.apiUrl}/api/payroll/summary/${year}/${month}`, {
      headers: this.headers()
    }).subscribe({
      next: (data) => this.summary = data
    });
  }

  createPayroll() {
    this.http.post<any>(`${environment.apiUrl}/api/payroll`, this.newPayroll, {
      headers: this.headers()
    }).subscribe({
      next: () => {
        this.loadPayrolls();
        this.loadSummary();
        this.showForm = false;
      }
    });
  }

  markAsPaid(id: number) {
    this.http.put(`${environment.apiUrl}/api/payroll/${id}/pay`, {}, {
      headers: this.headers()
    }).subscribe({
      next: () => this.loadPayrolls()
    });
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}