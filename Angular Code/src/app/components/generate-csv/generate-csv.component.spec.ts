import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenerateCsvComponent } from './generate-csv.component';

describe('GenerateCsvComponent', () => {
  let component: GenerateCsvComponent;
  let fixture: ComponentFixture<GenerateCsvComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GenerateCsvComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GenerateCsvComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
