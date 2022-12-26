import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExposTableComponent } from './expos-table.component';

describe('ExposTableComponent', () => {
  let component: ExposTableComponent;
  let fixture: ComponentFixture<ExposTableComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExposTableComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExposTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
