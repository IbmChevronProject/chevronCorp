import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DataService } from 'src/app/service/data.service';
import { LoginUser } from '../../classes/loginUser';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginUser: any = {};
  roleId: number;
  errorMessage: string;
  serviceLineId: number;
  constructor(private dataService: DataService, private router: Router) { }

  ngOnInit(): void {
  }

  ngOnDestroy() {
    this.dataService.userName = this.loginUser.userName;
  }

  getServiceLineId() {
    this.dataService.getServiceLineId(this.loginUser.userName).subscribe(
      (data: any) => {
        this.serviceLineId = data;
      },
      (error) => {
        console.log(error);
      });
  }

  getRoleIdFromLogin() {
    this.getServiceLineId();  // get Service Line Id
    this.dataService.getRoleIdFromLogin(this.loginUser.userName, this.loginUser.password).subscribe(
      (data) => {
        // do something, if upload success
        this.roleId = parseInt(data.toString());
        console.log("Success!")
        if (this.roleId == 1) {
          console.log(this.roleId)
          this.router.navigate(['/talent-acquisition'])
        }
        if (this.roleId == 2) {
          this.router.navigateByUrl('/service-line?serviceLineId=' + this.serviceLineId);
        }
        if (this.roleId == 3) {
          this.router.navigate(['/panel-board'])
        }
      },
      (error) => {
        this.errorMessage = "Invalid Username/ Password";
        console.log(error);
      });
  }
}
