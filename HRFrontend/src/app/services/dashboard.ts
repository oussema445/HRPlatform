import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment.development';
import { AuthService } from './auth';

@Injectable({ providedIn: 'root' })
export class DashboardService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient, private auth: AuthService) {}

  private headers() {
    return new HttpHeaders({
      'Authorization': `Bearer ${this.auth.getToken()}`
    });
  }

  getSummary() {
    return this.http.get<any>(`${this.apiUrl}/api/dashboard/summary`, { headers: this.headers() });
  }

  getByDepartment() {
    return this.http.get<any[]>(`${this.apiUrl}/api/dashboard/employees/departments`, { headers: this.headers() });
  }
}