import { TestBed } from '@angular/core/testing';

import { CoffeShopsService } from './coffee-shops.service';

describe('CoffeShopsService', () => {
  let service: CoffeShopsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CoffeShopsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
