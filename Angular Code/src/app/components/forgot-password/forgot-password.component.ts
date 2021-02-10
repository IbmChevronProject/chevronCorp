import { Component, OnInit } from '@angular/core';
import { Router, RouterStateSnapshot } from '@angular/router';
import { DataService } from 'src/app/service/data.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {

  userName: string;
  password: string;
  confirmPassword: string;

  constructor(private dataService: DataService, private router: Router) { }

  ngOnInit(): void {
    const snapshot: RouterStateSnapshot = this.router.routerState.snapshot;
    this.userName = snapshot.url.split('=')[1];
    if (this.userName === undefined)
      this.router.navigate(['/login']);
  }

  change(event) {
    this.password = event.target.value;
  }

  checkPass(event) {
    this.confirmPassword = event.target.value;
  }

  doPasswordsMatch() {
    if (this.password != undefined && this.confirmPassword != undefined)
      return this.password == this.confirmPassword ? true : false;
    else
      return true;
  }

  updatePassword() {
    this.dataService.updatePassword(this.userName, this.password).subscribe(
      (data) => {
        console.log("Success!")
      },
      (error) => {
        this.router.navigate(['/login']);
      });
  }
}
