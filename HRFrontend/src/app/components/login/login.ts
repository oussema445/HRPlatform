import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class LoginComponent {
  username = '';
  password = '';
  error = '';
  loading = false;

  constructor(private authService: AuthService, private router: Router) {}

 login() {
  this.loading = true;
  this.error = '';

  this.authService.login(this.username, this.password).subscribe({
  next: (res) => {
  this.authService.saveToken(res.token, res.role, res.fullName, res.employeeId);

  if (res.role === 'Employee') {
    this.router.navigate(['/leaves']);
  } else {
    this.router.navigate(['/dashboard']);
  }
},
    error: () => {
      this.error = 'Login ou mot de passe incorrect !';
      this.loading = false;
    }
  });
}
}