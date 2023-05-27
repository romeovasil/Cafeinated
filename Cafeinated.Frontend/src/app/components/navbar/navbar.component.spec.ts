import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AuthService } from '../../services/auth.service';
import { NavbarComponent } from './navbar.component';
import { By } from '@angular/platform-browser';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';
import {Router} from "@angular/router";

describe('NavbarComponent', () => {
  let component: NavbarComponent;
  let fixture: ComponentFixture<NavbarComponent>;
  let authService: AuthService;
  let router: Router;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NavbarComponent ],
      providers: [
        {
          provide: AuthService,
          useValue: {
            authStateChanged: of(true),
            logout: () => Promise.resolve(),
          }
        }
      ],
      imports: [RouterTestingModule]
    }).compileComponents();

    authService = TestBed.inject(AuthService);
    fixture = TestBed.createComponent(NavbarComponent);
    router = TestBed.get(Router);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should show the "Cafenele" link when user is logged in', () => {
    const cafeneleLinkDe = fixture.debugElement.query(By.css('a[routerLink="/cafenele"]'));
    expect(cafeneleLinkDe).not.toBeNull();
  });

  it('should call AuthService.logout() when logout is called', async () => {
    const authServiceSpy = spyOn(authService, 'logout').and.callThrough();
    await component.logout();
    expect(authServiceSpy).toHaveBeenCalled();
  });

  it('should not show the "Cont" link when user is logged in', () => {
    const contLinkDe = fixture.debugElement.query(By.css('a[href="/login"]'));
    expect(contLinkDe).toBeNull();
  });

  it('should show the "Comenzi" link when user is logged in', () => {
    const comenziLinkDe = fixture.debugElement.query(By.css('a[routerLink="/orders"]'));
    expect(comenziLinkDe).not.toBeNull();
  });


  it('should show the "Cosul tau" link when user is logged in', () => {
    const cosulTauLinkDe = fixture.debugElement.query(By.css('a[routerLink="/cart"]'));
    expect(cosulTauLinkDe).not.toBeNull();
  });



});
