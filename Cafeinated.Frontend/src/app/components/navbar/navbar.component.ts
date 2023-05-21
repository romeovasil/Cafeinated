import {Component, OnInit} from '@angular/core';
import {AuthService} from '../../services/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  isLoggedIn = false;

  constructor(
    private readonly _authService: AuthService,
    private readonly _router: Router
  ) {
    this._authService.authStateChanged.subscribe(async state => {
      this.isLoggedIn = state;
    });
  }

  async logout() {
    await this._authService.logout();
    this.isLoggedIn = false;
  }
}
