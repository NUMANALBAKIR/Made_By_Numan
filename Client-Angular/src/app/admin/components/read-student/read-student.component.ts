import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { StudentDTO } from 'src/app/admin/models/StudentDTO';
import { StudentsService } from 'src/app/admin/services/students.service';
import * as Highcharts from 'highcharts';


@Component({
  selector: 'app-read-student',
  templateUrl: './read-student.component.html',
  styleUrls: ['./read-student.component.css']
})
export class ReadStudentComponent implements OnInit, OnDestroy {

  constructor(private activatedRoute: ActivatedRoute, private studentsService: StudentsService) {
    this.studentId = 0;
    this.subscriptions = [];
    this.studentDTO = new StudentDTO();

  }


  studentId: number;
  subscriptions: Subscription[];
  // studentDTO!: Observable<StudentDTO>;
  studentDTO: StudentDTO;
  dataArr: any[] = [];

  // highcharts data
  Highcharts: typeof Highcharts = Highcharts; // required
  chartConstructor: string = 'chart'; // optional string, defaults to 'chart'
  chartOptions: Highcharts.Options = {
    title: {
      text: 'Marks obtained in Subjects',
      style: {
        fontSize: '20px'
      }
    },
    legend: {
      itemStyle: {
        fontSize: '20px'
      }
    },
    series: [{
      name: 'Subjects',
      colorByPoint: true,
      data: this.dataArr,
      type: 'column'
    }],
    xAxis: {
      type: 'category',
      labels: {
        style: {
          fontSize: '18px'
        }
      }
    },
    yAxis: {
      title: {
        text: 'Marks',
        style: {
          fontSize: '20px'
        }
      },
      labels: {
        style: {
          fontSize: '18px'
        }
      }
    },
    plotOptions: {
      series: {
        borderWidth: 0,
        dataLabels: {
          enabled: true,
          format: '{point.y:.1f}%',
          style:{
            fontSize: '18px'
          }
        }        
      }
    },
    tooltip: {
      headerFormat: '<span style="font-size:18px">Subject</span><br>',
      pointFormat: '<span style="font-size:18px; color:{point.color}">{point.name}</span>: <b style="font-size:18px;">{point.y:.2f}%</b><br/>'
    },
  }; // required
  updateFlag: boolean = false; // optional boolean
  oneToOneFlag: boolean = true; // optional boolean, defaults to false
  runOutsideAngular: boolean = false; // optional boolean, defaults to false



  ngOnInit(): void {
    this.subscriptions.push(
      this.activatedRoute.params.subscribe(
       (routeParams) => {
         this.studentId = Number(routeParams['studentId']);
       }
     )
    );

    // this.studentDTO = this.studentsService.getStudent(this.studentId);

    this.subscriptions.push(
      this.studentsService.getStudent(this.studentId).subscribe(
        (r: StudentDTO) => {
          this.studentDTO = r;
        },
        (e) => {
          console.log(e);
        }
      )
    );

    const subjects = [{
      subjectId: 1,
      subjectName: 'English',
      studentId: this.studentId,
      mark: 75
    },
    {
      subjectId: 2,
      subjectName: 'Math',
      studentId: this.studentId,
      mark: 25
    },
    {
      subjectId: 2,
      subjectName: 'Economics',
      studentId: this.studentId,
      mark: 95
    }, {
      subjectId: 2,
      subjectName: 'Biology',
      studentId: this.studentId,
      mark: 45
    },
    {
      subjectId: 3,
      subjectName: 'Accounting',
      studentId: this.studentId,
      mark: 90
    }];

    subjects.forEach(item => {
      this.dataArr.push(
        { name: item.subjectName, y: item.mark }
      );
    });


  }


  ngOnDestroy(): void {
    this.subscriptions.forEach(item => {
      item.unsubscribe();
    });
  }


}
