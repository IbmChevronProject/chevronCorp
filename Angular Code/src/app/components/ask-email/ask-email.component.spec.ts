import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AskEmailComponent } from './ask-email.component';

describe('AskEmailComponent', () => {
  let component: AskEmailComponent;
  let fixture: ComponentFixture<AskEmailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AskEmailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AskEmailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
