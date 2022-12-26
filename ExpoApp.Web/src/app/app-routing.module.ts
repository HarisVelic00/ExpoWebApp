import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ExpoCreateComponent } from './Components/expo-create/expo-create.component';
import { ExpoHomeComponent } from './Components/expo-home/expo-home.component';
import { LoginComponent } from './Components/login/login.component';
import { RegisterComponent } from './Components/register/register.component';
import { ApplyFormComponent } from './Components/apply-form/apply-form.component';
import { UserExposComponent } from './Components/user-expos/user-expos.component';
import { UpdateExpoComponent } from './Components/update-expo/update-expo.component';
import { LoggedGuard } from './guards/logged.guard';
import { AdminFormComponent } from './Components/admin-form/admin-form.component';
import { ExposTableComponent } from './Components/admin-form/expos-table/expos-table.component';
import { IndustryTableComponent } from './Components/admin-form/industry-table/industry-table.component';
import { UsersTableComponent } from './Components/admin-form/users-table/users-table.component';


const routes: Routes = [
  { path: '', component: LoginComponent },
  {
    path: 'home',
    component: ExpoHomeComponent,
  },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'organize', component: ExpoCreateComponent },
  { path: 'apply', component: ApplyFormComponent },
  { path: 'user-expos', component: UserExposComponent },
  { path: 'update/:id', component: UpdateExpoComponent },
  {
    path: 'adminPanel', children: [
      { path: 'expos-table', component: ExposTableComponent },
      { path: 'industry-table', component: IndustryTableComponent },
      { path: 'users-table', component: UsersTableComponent }
    ],
    component: AdminFormComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
