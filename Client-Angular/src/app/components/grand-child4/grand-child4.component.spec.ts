import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GrandChild4Component } from './grand-child4.component';

describe('GrandChild4Component', () => {
  let component: GrandChild4Component;
  let fixture: ComponentFixture<GrandChild4Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GrandChild4Component ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GrandChild4Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
