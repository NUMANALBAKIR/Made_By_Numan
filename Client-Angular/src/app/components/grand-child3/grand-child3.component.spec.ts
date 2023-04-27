import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GrandChild3Component } from './grand-child3.component';

describe('GrandChild3Component', () => {
  let component: GrandChild3Component;
  let fixture: ComponentFixture<GrandChild3Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GrandChild3Component ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GrandChild3Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
