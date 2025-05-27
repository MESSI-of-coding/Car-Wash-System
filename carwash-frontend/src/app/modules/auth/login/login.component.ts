//Path: CarWash/carwash-frontend/src/app/modules/auth/login.component.ts
//Purpose: This code defines a LoginComponent in Angular that handles user login functionality. 
// It uses Reactive Forms for form handling and validation, and it interacts with an AuthService to perform the login operation. 
// Upon successful login, it stores the JWT token in local storage and redirects the user based on their role (admin, washer, or customer).

import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  onSubmit() {
    if (this.loginForm.invalid) return;

    const { email, password } = this.loginForm.value;

    this.authService.login(email, password).subscribe({
      next: (res) => {
        const token = res.token;
        localStorage.setItem('token', token);

        const payload = JSON.parse(atob(token.split('.')[1]));
        const role = payload.role;

        if (role === 'admin') {
          this.router.navigate(['/admin']);
        } else if (role === 'washer') {
          this.router.navigate(['/washer']);
        } else {
          this.router.navigate(['/customer']);
        }
      },
      error: (err) => {
        console.error('Login failed', err);
      },
    });
  }
}
