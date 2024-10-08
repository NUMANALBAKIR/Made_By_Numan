import { Country } from "./country";
import { Subject } from "./subject";

export class Student {

  studentId: number | any;
  name: string | any;
  dateOfBirth: string | any;
  passed: boolean | any;  // checkbox ... act
  gender: string | any;   // radio ... sta

  countryId: number | any;    // cli...
  country: Country;     // cli...
  subjects: Subject[] | undefined;


  constructor() {
    this.studentId = null;
    this.name = null;
    this.dateOfBirth = null;
    this.passed = true;
    this.gender = null;

    this.countryId = null; 
    this.country = new Country(); // imp
    this.subjects = []; // imp

  }

}
