import { SubjectCreateDTO } from "./subjectCreateDTO";

export class StudentCreateDTO {

  studentId: number | any; // just practise
  name: string | any;
  dateOfBirth: string | any;
  passed: boolean | any;  // checkbox ... act
  gender: string | any;   // radio ... sta

  countryId: number | any;    // cli...

  subjects: SubjectCreateDTO[];

  
  constructor() {
    this.studentId = null;
    this.name = null;
    this.dateOfBirth = null;
    this.passed = true;
    this.gender = null;

    this.countryId = null;

    this.subjects = [];

  }

}
