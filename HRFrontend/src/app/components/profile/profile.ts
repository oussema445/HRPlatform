import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../services/auth';
import { Router } from '@angular/router';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './profile.html',
  styleUrl: './profile.scss'
})
export class ProfileComponent {
  fullName = '';
  role = '';
  oldPassword = '';
  newPassword = '';
  confirmPassword = '';
  message = '';
  error = '';

  constructor(
    private authService: AuthService,
    private router: Router
  ) {
    this.fullName = this.authService.getFullName() || '';
    this.role = this.authService.getRole() || '';
  }

  changePassword() {
    this.message = '';
    this.error = '';

    if (this.newPassword !== this.confirmPassword) {
      this.error = 'Les mots de passe ne correspondent pas !';
      return;
    }

    if (this.newPassword.length < 4) {
      this.error = 'Le mot de passe doit contenir au moins 4 caractères !';
      return;
    }

    this.authService.changePassword(this.oldPassword, this.newPassword).subscribe({
      next: () => {
        this.message = 'Mot de passe modifié avec succès !';
        this.oldPassword = '';
        this.newPassword = '';
        this.confirmPassword = '';
      },
      error: (err) => {
        this.error = err.error || 'Erreur lors de la modification !';
      }
    });
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}