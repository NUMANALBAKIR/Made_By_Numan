import { TestBed } from '@angular/core/testing';

import { SubjectsListsService } from './subjects-lists.service';

describe('SubjectsListsService', () => {
  let service: SubjectsListsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SubjectsListsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
