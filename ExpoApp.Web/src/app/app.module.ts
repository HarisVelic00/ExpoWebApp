import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { ExpoCreateComponent } from './Components/expo-create/expo-create.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { JwtService } from './services/jwt.service';

// Components
import { AppComponent } from './app.component';
import { HeaderComponent } from './Components/header/header.component';
import { ExpoHomeComponent } from './Components/expo-home/expo-home.component';
import { LoginComponent } from './Components/login/login.component';
import { RegisterComponent } from './Components/register/register.component';
import { FilterComponent } from './Components/filter/filter.component';
import { ExpoCardComponent } from './Components/expo-card/expo-card.component';
import { ApplyFormComponent } from './Components/apply-form/apply-form.component';
import { UsersProfileComponent } from './Components/users-profile/users-profile.component';
import { UserExposComponent } from './Components/user-expos/user-expos.component';
import { UpdateExpoComponent } from './Components/update-expo/update-expo.component';
import { NotificationsComponent } from './Components/notifications/notifications.component';
import { HeaderStatusComponent } from './Components/header-status/header-status.component';
import { AdminFormComponent } from './Components/admin-form/admin-form.component';
import { EditIndustryComponent } from './Components/admin-form/edit-industry/edit-industry.component';
import { IndustryTableComponent } from './Components/admin-form/industry-table/industry-table.component';
import { UsersTableComponent } from './Components/admin-form/users-table/users-table.component';
import { ExposTableComponent } from './Components/admin-form/expos-table/expos-table.component';
import { EditExposComponent } from './Components/admin-form/edit-expos/edit-expos.component';
import { EditUsersComponent } from './Components/admin-form/edit-users/edit-users.component';


// Angular material
import { MatCardModule } from '@angular/material/card';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSliderModule } from '@angular/material/slider';
import { MatIconModule } from '@angular/material/icon';
import { MatDividerModule } from '@angular/material/divider';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatTabsModule } from '@angular/material/tabs';
import { MatSelectModule } from '@angular/material/select';
import { MatStepperModule } from '@angular/material/stepper';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatBadgeModule } from '@angular/material/badge';
import { PorukeComponent } from './Components/poruke/poruke.component';
import {MatDialogModule} from '@angular/material/dialog';
import { PorukaUpdateComponent } from './Components/poruke/poruka-update/poruka-update.component';
import { MatSortModule } from '@angular/material/sort';

@NgModule({
  declarations: [
    AppComponent,
    ExpoHomeComponent,
    LoginComponent,
    RegisterComponent,
    ExpoCreateComponent,
    HeaderComponent,
    FilterComponent,
    ExpoCardComponent,
    ApplyFormComponent,
    UsersProfileComponent,
    UserExposComponent,
    UpdateExpoComponent,
    NotificationsComponent,
    HeaderStatusComponent,
    AdminFormComponent,
    EditIndustryComponent,
    IndustryTableComponent,
    UsersTableComponent,
    EditExposComponent,
    EditUsersComponent,
    ExposTableComponent,
    PorukeComponent,
    PorukaUpdateComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatCardModule,
    MatCheckboxModule,
    MatSliderModule,
    MatIconModule,
    MatDividerModule,
    MatExpansionModule,
    MatFormFieldModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatInputModule,
    MatRadioModule,
    MatGridListModule,
    MatButtonModule,
    MatTableModule,
    MatTabsModule,
    MatSelectModule,
    MatStepperModule,
    MatSnackBarModule,
    MatBadgeModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSortModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: JwtService,
    multi: true,
  },],
  bootstrap: [AppComponent],
  entryComponents:[EditIndustryComponent]
})
export class AppModule {}
