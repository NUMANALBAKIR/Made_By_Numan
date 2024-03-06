// import { DashboardService } from '../../../services/dashboard.service';
import { AfterViewChecked, AfterViewInit, Component, ElementRef, OnInit, Renderer2, ViewChild } from '@angular/core';
import { TestService } from '../services/test.service';
import { BehaviorSubject } from 'rxjs';
import { SharedService } from '../services/shared.service';

@Component({
  selector: 'app-test',
  templateUrl: './test.component.html',
  styleUrls: ['./test.component.css']
})
export class TestComponent implements AfterViewInit, AfterViewChecked {





  MONTHS = this.sharedService.MONTHS_BS.asObservable();

  trackByFn(i: number, item: any) {
    return item.Name;
  }

  //-----

  @ViewChild('test') test: ElementRef | any;

  constructor(private testService: TestService,
    private renderer: Renderer2,
    private sharedService: SharedService
  ) {


    this.sharedService.getMonths().subscribe(
      (r) => {
        this.sharedService.MONTHS_BS.next(
          r // this is like: .next(r)
        );
      }
    );
  }

  ngAfterViewInit(): void {
    let el = this.test.nativeElement;

    // this.renderer.setStyle(el, 'backgroundColor', 'red');
    this.renderer.listen(el, 'click', () => { alert('clicked'); })
    this.renderer.addClass(el, 'mb-5');
    el.innerText = 'sss';

    const text = this.renderer.createText('Click here to add ');
    this.renderer.appendChild(el, text);
    this.renderer.setAttribute(el, 'class', 'text-warning');

  }

  ngAfterViewChecked(): void {
    // alert('ngAfterViewChecked');
  }


  ngAftrVw() {
    let el = this.test.nativeElement;
    this.renderer.setStyle(el, 'backgroundColor', 'blue');
  }

  justEvent(): void {
    alert('justEvent');
  }


  // Designation: string;
  // Username: string;
  // NoOfTeamMembers: number;
  // TotalCostOfAllProjects: number;
  // PendingTasks: number;
  // UpComingProjects: number;
  // ProjectCost: number;
  // CurrentExpenditure: number;
  // AvailableFunds: number;
  // ToDay: Date;

  // Clients: string[];
  // Projects: string[];
  // Years: number[] = [];
  // TeamMembersSummary = [];
  // TeamMembers = [];

  // constructor(private dashboardService: DashboardService) {
  // }

  // ngOnInit() {
  //   this.Designation = "Team Leader";
  //   this.Username = "Scott Smith";
  //   this.NoOfTeamMembers = 67;
  //   this.TotalCostOfAllProjects = 240;
  //   this.PendingTasks = 15;
  //   this.UpComingProjects = 0.2;
  //   this.ProjectCost = 2113507;
  //   this.CurrentExpenditure = 96788;
  //   this.AvailableFunds = 52536;
  //   this.ToDay = new Date();

  //   this.Clients = [
  //     "ABC Infotech Ltd.", "DEF Software Solutions", "GHI Industries"
  //   ];

  //   this.Projects = [
  //     "Project A", "Project B", "Project C", "Project D"
  //   ];

  //   for (var i = 2019; i >= 2010; i--) {
  //     this.Years.push(i);
  //   }

  //   this.TeamMembersSummary = this.dashboardService.getTeamMembersSummary();


  //   this.TeamMembers = [
  //     {
  //       Region: "East", Members: [
  //         { ID: 1, Name: "Ford", Status: "Available" },
  //         { ID: 2, Name: "Miller", Status: "Available" },
  //         { ID: 3, Name: "Jones", Status: "Busy" },
  //         { ID: 4, Name: "James", Status: "Busy" }
  //       ]
  //     },
  //     {
  //       Region: "West", Members: [
  //         { ID: 5, Name: "Anna", Status: "Available" },
  //         { ID: 6, Name: "Arun", Status: "Available" },
  //         { ID: 7, Name: "Varun", Status: "Busy" },
  //         { ID: 8, Name: "Jasmine", Status: "Busy" }
  //       ]
  //     },
  //     {
  //       Region: "South", Members: [
  //         { ID: 9, Name: "Krishna", Status: "Available" },
  //         { ID: 10, Name: "Mohan", Status: "Available" },
  //         { ID: 11, Name: "Raju", Status: "Busy" },
  //         { ID: 12, Name: "Farooq", Status: "Busy" }
  //       ]
  //     },
  //     {
  //       Region: "North", Members: [
  //         { ID: 13, Name: "Jacob", Status: "Available" },
  //         { ID: 14, Name: "Smith", Status: "Available" },
  //         { ID: 15, Name: "Jones", Status: "Busy" },
  //         { ID: 16, Name: "James", Status: "Busy" }
  //       ]
  //     }
  //   ];
  // }

  // onProjectChange($event) {
  //   if ($event.target.innerHTML == "Project A") {
  //     this.ProjectCost = 2113507;
  //     this.CurrentExpenditure = 96788;
  //     this.AvailableFunds = 52436;
  //   }
  //   else if ($event.target.innerHTML == "Project B") {
  //     this.ProjectCost = 88923;
  //     this.CurrentExpenditure = 22450;
  //     this.AvailableFunds = 2640;
  //   }
  //   else if ($event.target.innerHTML == "Project C") {
  //     this.ProjectCost = 662183;
  //     this.CurrentExpenditure = 7721;
  //     this.AvailableFunds = 9811;
  //   }
  //   else if ($event.target.innerHTML == "Project D") {
  //     this.ProjectCost = 928431;
  //     this.CurrentExpenditure = 562;
  //     this.AvailableFunds = 883;
  //   }
  // }
}
