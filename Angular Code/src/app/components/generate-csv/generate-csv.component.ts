import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/app/service/data.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-generate-csv',
  templateUrl: './generate-csv.component.html',
  styleUrls: ['./generate-csv.component.css']
})
export class GenerateCsvComponent implements OnInit {

  constructor(private dataService: DataService) { }
  json: any = [];
  ngOnInit(): void {
  }

  sendMailToTA() {
    this.dataService.connectToGenerateCSVLogicApp(this.json).subscribe(
      (data) => {
        console.log("Success!")
      },
      (error) => {
        console.log(error);
      });
  }

  getInterviewDetailsForCSV() {
    this.dataService.getInterviewDetailsForCSV().subscribe(
      (data) => {
        this.json = data;
        this.sendMailToTA();
        Swal.fire('Great!', 'Interview details has been mailed!', 'success')
      },
      (error) => {
        console.log(error);
      });
  }
}
