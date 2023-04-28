import { Country } from "./country";
import { SubjectDTO } from "./subjectDTO";

export class StudentDTO {

  studentId: number | any;
  name: string | any;
  dateOfBirth: string | any;
  passed: boolean | any;  // checkbox ... act
  gender: string | any;   // radio ... sta

  countryId: number | any;    // cli...
  country: Country;     // cli...

  subjects: SubjectDTO[];

  
  constructor() {
    this.studentId = null;
    this.name = null;
    this.dateOfBirth = null;
    this.passed = true;
    this.gender = null;

    this.countryId = null;
    this.country = new Country(); // important

    this.subjects = [];
  }

}
