import { TestBed } from '@angular/core/testing';

import { OrganizerUpdateGuard } from './organizer-update.guard';

describe('OrganizerUpdateGuard', () => {
  let guard: OrganizerUpdateGuard;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    guard = TestBed.inject(OrganizerUpdateGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });
});
