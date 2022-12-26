import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ExpoHomeComponent } from '../expo-home/expo-home.component';
import { ExposTableComponent } from './expos-table/expos-table.component';
import { IndustryTableComponent } from './industry-table/industry-table.component';
import { UsersTableComponent } from './users-table/users-table.component';

const routes: Routes = [
  { path: 'expos-table', component: ExposTableComponent, outlet:'exposTable' },
  { path: 'industry-table', component: IndustryTableComponent },
  { path: 'users-table', component: UsersTableComponent },
  { path: 'home', component: ExpoHomeComponent, },
];


@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminFormRoutingModule { }
