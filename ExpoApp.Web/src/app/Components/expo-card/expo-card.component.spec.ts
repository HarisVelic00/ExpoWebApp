import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpoCardComponent } from './expo-card.component';

describe('ExpoCardComponent', () => {
  let component: ExpoCardComponent;
  let fixture: ComponentFixture<ExpoCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExpoCardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExpoCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
