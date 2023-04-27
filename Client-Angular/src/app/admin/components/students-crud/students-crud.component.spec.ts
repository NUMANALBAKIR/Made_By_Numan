import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StudentsCRUDComponent } from './students-crud.component';

describe('StudentsCRUDComponent', () => {
  let component: StudentsCRUDComponent;
  let fixture: ComponentFixture<StudentsCRUDComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StudentsCRUDComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StudentsCRUDComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
