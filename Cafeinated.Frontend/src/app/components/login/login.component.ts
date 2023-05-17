import { Component } from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {MatSnackBar} from '@angular/material/snack-bar';
import {AuthService} from '../../services/auth.service';
import {firstValueFrom} from 'rxjs';
import {Login} from '../../common/login';
import {Router} from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginForm = this._formBuilder.group({
    email: ['', [Validators.email, Validators.required]],
    password: ['', [Validators.required]]
  })

  constructor(
    private readonly _formBuilder: FormBuilder,
    private readonly _authService: AuthService,
    private readonly _snack: MatSnackBar,
    private readonly _router: Router
  ) {
  }

  async submit(): Promise<void> {
    this.loginForm.markAllAsTouched();

    if (this.loginForm.invalid) {
      this._snack.open('Formularul este invalid!', 'Close');
      return;
    }

    try {
      await this._authService.login(this.loginForm.value as Login);
      this._snack.open('Autentificarea a avut succes!', 'Close');
      await this._router.navigate(['/cafenele']);
    } catch (err) {
      this._snack.open('Oops! Ceva nu a functionat!', 'Close');
    }
  }
}
