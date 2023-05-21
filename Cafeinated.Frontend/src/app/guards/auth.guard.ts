import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot} from '@angular/router';
import {AuthService} from '../services/auth.service';
import {MatSnackBar} from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private readonly _authService: AuthService,
              private readonly _router: Router,
              private readonly _snack: MatSnackBar) {
  }

  async canActivate(): Promise<boolean> {
    const isLoggedIn = await this._authService.authStateAsync;

    if (!isLoggedIn) {
      this._router.navigate(['login']).then();
      this._snack.open('You are not allowed to access that page.', 'Close');
      return false;
    }
    return true;
  }
}
