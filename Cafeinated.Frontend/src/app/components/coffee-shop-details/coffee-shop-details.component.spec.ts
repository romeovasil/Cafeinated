import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CoffeeShopDetailsComponent } from './coffee-shop-details.component';

describe('CoffeeShopDetailsComponent', () => {
  let component: CoffeeShopDetailsComponent;
  let fixture: ComponentFixture<CoffeeShopDetailsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CoffeeShopDetailsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CoffeeShopDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
