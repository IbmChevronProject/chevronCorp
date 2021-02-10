import { Component, OnInit } from '@angular/core';
import { DataService } from 'src/app/service/data.service';
import Swal from 'sweetalert2/dist/sweetalert2.js';

@Component({
  selector: 'app-ask-email',
  templateUrl: './ask-email.component.html',
  styleUrls: ['./ask-email.component.css']
})
export class AskEmailComponent implements OnInit {

  email: string;
  checkEmailFlag: boolean;
  errorMessage: string;
  constructor(private dataService: DataService) { }

  ngOnInit(): void {
  }

  change(event) {
    this.email = event.target.value;
    this.checkIfEmailExists();
  }

  checkIfEmailExists() {
    this.dataService.checkIfEmailExists(this.email).subscribe(
      (data: any) => {
        this.checkEmailFlag = data;
        console.log(this.checkEmailFlag)
        if (this.checkEmailFlag == false)
          this.errorMessage = "Email does not exist!";
        else
          this.errorMessage = null;
      },
      (error) => {
        console.log(error);
      });
  }

  connectToLogicApp() {
    if (this.checkEmailFlag) {
      this.dataService.connectToForgotPasswordLogicApp(this.email).subscribe(
        (data) => {
          console.log("Success!")
        },
        (error) => {
          Swal.fire('Great!', 'Password Reset mail has been sent to' + this.email, 'success')
          console.log(error);
        });
    }
  }
}
