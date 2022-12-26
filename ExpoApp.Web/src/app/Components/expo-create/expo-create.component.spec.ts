import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpoCreateComponent } from './expo-create.component';

describe('ExpoCreateComponent', () => {
  let component: ExpoCreateComponent;
  let fixture: ComponentFixture<ExpoCreateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExpoCreateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExpoCreateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
