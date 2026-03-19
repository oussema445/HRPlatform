import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment.development';
import { AuthService } from './auth';

@Injectable({ providedIn: 'root' })
export class EmployeeService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient, private auth: AuthService) {}

  private headers() {
    return new HttpHeaders({
      'Authorization': `Bearer ${this.auth.getToken()}`
    });
  }

  getAll() {
    return this.http.get<any[]>(`${this.apiUrl}/api/employee`, { headers: this.headers() });
  }

  getById(id: number) {
    return this.http.get<any>(`${this.apiUrl}/api/employee/${id}`, { headers: this.headers() });
  }

  create(employee: any) {
    return this.http.post<any>(`${this.apiUrl}/api/employee`, employee, { headers: this.headers() });
  }

  update(id: number, employee: any) {
    return this.http.put<any>(`${this.apiUrl}/api/employee/${id}`, employee, { headers: this.headers() });
  }

  delete(id: number) {
    return this.http.delete(`${this.apiUrl}/api/employee/${id}`, { headers: this.headers() });
  }
}