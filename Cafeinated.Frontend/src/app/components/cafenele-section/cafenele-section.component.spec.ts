import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CafeneleSectionComponent } from './cafenele-section.component';

describe('CafeneleSectionComponent', () => {
  let component: CafeneleSectionComponent;
  let fixture: ComponentFixture<CafeneleSectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CafeneleSectionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CafeneleSectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
