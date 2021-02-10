import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { LoginComponent } from './components/login/login.component';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AskEmailComponent } from './components/ask-email/ask-email.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { TalentAcquisitionComponent } from './components/talent-acquisition/talent-acquisition.component';
import { ServiceLineComponent } from './components/service-line/service-line.component';
import { CommonModule, DatePipe } from '@angular/common';
import { PanelistComponent } from './components/panelist/panelist.component';
import { NavbarComponent } from './components/navbar/navbar.component';
import { UploadFeedbackComponent } from './components/upload-feedback/upload-feedback.component';
import { GenerateCsvComponent } from './components/generate-csv/generate-csv.component';

@NgModule({
  declarations: [
    AppComponent,
    RegistrationComponent,
    LoginComponent,
    AskEmailComponent,
    ForgotPasswordComponent,
    TalentAcquisitionComponent,
    ServiceLineComponent,
    PanelistComponent,
    NavbarComponent,
    UploadFeedbackComponent,
    GenerateCsvComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    CommonModule
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
