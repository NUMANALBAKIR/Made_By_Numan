import { TestBed } from '@angular/core/testing';

import { ComponentCommunicationsService } from './component-communications.service';

describe('ComponentCommunicationsService', () => {
  let service: ComponentCommunicationsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ComponentCommunicationsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
