import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment.development';
import { AuthService } from './auth';

@Injectable({ providedIn: 'root' })
export class LeaveService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient, private auth: AuthService) {}

  private headers() {
    return new HttpHeaders({
      'Authorization': `Bearer ${this.auth.getToken()}`
    });
  }

  getAll() {
    return this.http.get<any[]>(`${this.apiUrl}/api/leave`, { headers: this.headers() });
  }

  create(leave: any) {
    return this.http.post<any>(`${this.apiUrl}/api/leave`, leave, { headers: this.headers() });
  }

  updateStatus(id: number, status: string) {
    return this.http.put<any>(`${this.apiUrl}/api/leave/${id}/status`, { status }, { headers: this.headers() });
  }
}