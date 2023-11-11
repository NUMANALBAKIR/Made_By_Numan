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

    this.highcharts = Highcharts; // required
    this.chartConstructor = 'chart'; // optional string, defaults to 'chart'
    // chartOptions: any; // no need
    this.updateFlag = false;
    this.oneToOneFlag = true;
    this.runOutsideAngular = false;

  }


  studentId: number;
  subscriptions: Subscription[];
  studentDTO: StudentDTO;
  dataArr: any[] = [];

  // highcharts data
  highcharts: typeof Highcharts;// required
  chartConstructor: string; // optional string, defaults to 'chart'
  chartOptions: any;
  updateFlag: boolean;
  oneToOneFlag: boolean;
  runOutsideAngular: boolean;


  ngOnInit(): void {
    this.subscriptions.push(  // multiple  subscriptions

      this.activatedRoute.params.subscribe(
        (routeParams) => {
          this.studentId = Number(routeParams['studentId']);  // note Number
        }
      ),

      this.studentsService.getStudent(this.studentId).subscribe(
        (response: StudentDTO) => {
          this.studentDTO = response;
          this.drawHighChart();   // note: after getting response
        },
        (e) => {
          console.log(e);
        },
        () => {
          console.log('complete');
        }
      )

    );

  }


  drawHighChart() {
    // populate data prop of highchart
    this.studentDTO.subjects.forEach(item => {
      this.dataArr.push(
        { name: item.subjectName, y: item.mark }
      );
    });

    this.chartOptions = {
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
            style: {
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

    this.updateFlag = true; // this updates chart
  }


  ngOnDestroy(): void {
    this.subscriptions.forEach(item => {
      item.unsubscribe();
    });
  }


}
