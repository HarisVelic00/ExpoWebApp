import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditExposComponent } from './edit-expos.component';

describe('EditExposComponent', () => {
  let component: EditExposComponent;
  let fixture: ComponentFixture<EditExposComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditExposComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditExposComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
