import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment.development';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  login(username: string, password: string) {
    return this.http.post<any>(`${this.apiUrl}/api/auth/login`, { username, password });
  }
saveToken(token: string, role: string, fullName: string, employeeId?: number) {
  localStorage.setItem('token', token);
  localStorage.setItem('role', role);
  localStorage.setItem('fullName', fullName);
  if (employeeId) localStorage.setItem('employeeId', employeeId.toString());
}

  getToken() { return localStorage.getItem('token'); }
  getRole() { return localStorage.getItem('role'); }
  getFullName() { return localStorage.getItem('fullName'); }

  isLoggedIn() { return !!this.getToken(); }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    localStorage.removeItem('fullName');
  }
  changePassword(oldPassword: string, newPassword: string) {
  return this.http.put<any>(
    `${this.apiUrl}/api/auth/change-password`,
    { oldPassword, newPassword },
    { headers: new HttpHeaders({ 'Authorization': `Bearer ${this.getToken()}` }) }
  );
}
}