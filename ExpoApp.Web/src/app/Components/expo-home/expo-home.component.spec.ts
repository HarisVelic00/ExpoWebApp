import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpoHomeComponent } from './expo-home.component';

describe('ExpoHomeComponent', () => {
  let component: ExpoHomeComponent;
  let fixture: ComponentFixture<ExpoHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExpoHomeComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExpoHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
