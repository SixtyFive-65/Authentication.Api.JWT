import { Component } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { NgForm } from '@angular/forms';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
})
export class RegisterComponent {
  username: string = '';
  email: string = '';
  mobilenumber: string = '';
  password: string = '';
  registerError: boolean = false;
  registerErrorMessage: string = '';

  constructor(private authService: AuthService) {}

  onSubmit(form: NgForm) {
    if(form.valid){

    this.authService.register(this.username, this.email, this.mobilenumber, this.password).subscribe({
      next: (response) => {
        console.log('Registration successful', response);
        localStorage.setItem('token', response.token); 
      },
      error: (error) => {
        if (error.status === 400) {
          this.registerError = true;
          this.registerErrorMessage = error.error.errorMessage;
        }
        console.error('Registration failed', error);
      },
    });
  }else{
    console.error('Form is invalid')
  }
  }

  onMobileNumberInput(event: any): void {
    const input = event.target as HTMLInputElement;
    input.value = input.value.replace(/\D/g, ''); 
  }
}