import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment.development';
import { AuthService } from './auth';

@Injectable({ providedIn: 'root' })
export class NotificationService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient, private auth: AuthService) {}

  private headers() {
    return new HttpHeaders({
      'Authorization': `Bearer ${this.auth.getToken()}`
    });
  }

  getUnread() {
    return this.http.get<any>(`${this.apiUrl}/api/notification/unread`, { headers: this.headers() });
  }

  getAll() {
    return this.http.get<any[]>(`${this.apiUrl}/api/notification`, { headers: this.headers() });
  }

  markAsRead(id: number) {
    return this.http.put<any>(`${this.apiUrl}/api/notification/${id}/read`, {}, { headers: this.headers() });
  }

  markAllAsRead() {
    return this.http.put<any>(`${this.apiUrl}/api/notification/read-all`, {}, { headers: this.headers() });
  }
  getByEmployee(employeeId: number) {
  return this.http.get<any>(
    `${this.apiUrl}/api/notification/employee/${employeeId}`,
    { headers: this.headers() }
  );
}
}