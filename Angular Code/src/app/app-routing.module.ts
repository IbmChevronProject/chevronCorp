import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegistrationComponent } from './components/registration/registration.component';
import { LoginComponent } from './components/login/login.component';
import { AskEmailComponent } from './components/ask-email/ask-email.component';
import { ForgotPasswordComponent } from './components/forgot-password/forgot-password.component';
import { TalentAcquisitionComponent } from './components/talent-acquisition/talent-acquisition.component';
import { ServiceLineComponent } from './components/service-line/service-line.component';
import { PanelistComponent } from './components/panelist/panelist.component';
import { GenerateCsvComponent } from './components/generate-csv/generate-csv.component';
import { UploadFeedbackComponent } from './components/upload-feedback/upload-feedback.component';

const routes: Routes = [
  { path: 'register', component: RegistrationComponent },
  { path: '', redirectTo: '/register', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'password-reset', component: AskEmailComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: 'talent-acquisition', component: TalentAcquisitionComponent },
  { path: 'service-line', component: ServiceLineComponent },
  { path: 'panel-board', component: PanelistComponent },
  { path: 'generate-csv', component: GenerateCsvComponent },
  { path: 'upload-feedback', component: UploadFeedbackComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule { }
