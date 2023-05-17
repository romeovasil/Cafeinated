import { Component } from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {AuthService} from '../../services/auth.service';
import {MatSnackBar} from '@angular/material/snack-bar';
import {Register} from '../../common/register';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent {
  showPopup = false;
  openPopup() {
    this.showPopup = true;
  }

  closePopup() {
    this.showPopup = false;
  }

  registerForm = this._formBuilder.group({
    email: ['', [Validators.email, Validators.required]],
    password: ['', [Validators.required]],
    confirmPassword: ['', [Validators.required]],
    firstName: ['', [Validators.required]],
    lastName: ['', [Validators.required]],
    address: ['', [Validators.required]],
    phoneNumber: ['', [Validators.required]]
  });

  constructor(
    private readonly _formBuilder: FormBuilder,
    private readonly _authService: AuthService,
    private readonly _snack: MatSnackBar
  ) {
  }

  async submit(): Promise<void> {
    this.registerForm.markAllAsTouched();

    if (this.registerForm.invalid) {
      this._snack.open('Formularul este invalid!', 'Close');
      return;
    }

    try {
      await this._authService.register(this.registerForm.value as Register);
      this._snack.open('Inregistrarea a avut succes!', 'Close');
      this.showPopup = false;
    } catch (err) {
      this._snack.open('Oops! Ceva nu a functionat!', 'Close');
    }
  }
}
