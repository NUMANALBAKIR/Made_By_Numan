import { Country } from "./country";
import { Subject } from "./subject";
import { SubjectUpateDTO } from "./subjectUpateDTO";


export class StudentUpdateDTO {

  studentId: number | any;
  name: string | any;
  dateOfBirth: string | any;
  passed: boolean | any;  // checkbox ... act
  gender: string | any;   // radio ... sta

  countryId: number | any;    // cli...

  subjects: SubjectUpateDTO[];
  

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
