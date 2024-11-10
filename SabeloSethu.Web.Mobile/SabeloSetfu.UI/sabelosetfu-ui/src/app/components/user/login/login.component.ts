import { Component } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  loginError: boolean = false;
  loginErrorMessage: string = '';


  constructor(private authService: AuthService) {}

  onSubmit(form: NgForm) {
    if (form.valid) {
    this.authService.login(this.username, this.password).subscribe({
      next: (response) => {
        console.log('Login successful', response.token);

        localStorage.setItem('token', response.token); 
      },
      error: (error) => {
        if (error.status === 401) {
          this.loginError = true; 
          this.loginErrorMessage = error.error.errorMessage;
        }
        console.error('Login failed', error);
      },
    });
  }else{
    console.error('Form is invalid');
  }
  }
}