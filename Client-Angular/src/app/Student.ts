import { Country } from "./country";

export class Student {

  studentId: number | any;
  name: string | any;
  dateOfBirth: string | any;
  passed: boolean | any;  // checkbox ... act
  gender: string | any;   // radio ... sta

  countryId: number | any;    // cli...
  country: Country | any;     // cli...

  // subjectsListId: number | any;
  // subjectsList: SubjectsList | any;

  constructor() {
    this.studentId = null;
    this.name = null;
    this.dateOfBirth = null;
    this.passed = true;
    this.gender = null;

    this.countryId = null;
    this.country = new Country();

    // this.subjectsListId = null;
    // this.subjectsList = new SubjectsList();
  }

}
