import { Country } from "../country";


export class StudentUpdateDTO {

  studentId: number | any;
  name: string | any;
  dateOfBirth: string | any;
  passed: boolean | any;  // checkbox ... act
  gender: string | any;   // radio ... sta

  countryId: number | any;    // cli...
  country: Country;     // cli...

  //subjects: Subject[];

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

    //this.subjects = [];

    // this.subjectsListId = null;
    // this.subjectsList = new SubjectsList();
  }

}