import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PorukaUpdateComponent } from './poruka-update.component';

describe('PorukaUpdateComponent', () => {
  let component: PorukaUpdateComponent;
  let fixture: ComponentFixture<PorukaUpdateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PorukaUpdateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PorukaUpdateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
