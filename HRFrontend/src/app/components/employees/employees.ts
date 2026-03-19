import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { EmployeeService } from '../../services/employee';
import { AuthService } from '../../services/auth';
import { Router } from '@angular/router';

@Component({
  selector: 'app-employees',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './employees.html',
  styleUrl: './employees.scss'
})
export class EmployeesComponent implements OnInit {
  employees: any[] = [];
  showForm = false;
  isEdit = false;
  role = '';

  newEmployee = {
    fullName: '',
    email: '',
    phone: '',
    position: '',
    department: '',
    hireDate: '',
    salary: 0,
    status: 'Active'
  };

  constructor(
    private employeeService: EmployeeService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    this.role = this.authService.getRole() || '';
    this.loadEmployees();
  }

  loadEmployees() {
    this.employeeService.getAll().subscribe({
      next: (data) => this.employees = data,
      error: () => this.router.navigate(['/login'])
    });
  }

  openForm() {
    this.showForm = true;
    this.isEdit = false;
    this.newEmployee = {
      fullName: '', email: '', phone: '',
      position: '', department: '',
      hireDate: '', salary: 0, status: 'Active'
    };
  }

  editEmployee(emp: any) {
    this.showForm = true;
    this.isEdit = true;
    this.newEmployee = { ...emp };
  }

  saveEmployee() {
    if (this.isEdit) {
      this.employeeService.update((this.newEmployee as any).id, this.newEmployee).subscribe({
        next: () => { this.loadEmployees(); this.showForm = false; }
      });
    } else {
      this.employeeService.create(this.newEmployee).subscribe({
        next: () => { this.loadEmployees(); this.showForm = false; }
      });
    }
  }

  deleteEmployee(id: number) {
    if (confirm('Supprimer cet employé ?')) {
      this.employeeService.delete(id).subscribe({
        next: () => this.loadEmployees()
      });
    }
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}