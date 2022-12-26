import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { UsersVM } from '../../../models/UsersVM';
import { UsersService } from '../../../services/users/users.service';
import { PorukeComponent } from '../../poruke/poruke.component';

@Component({
  selector: 'app-users-table',
  templateUrl: './users-table.component.html',
  styleUrls: ['./users-table.component.css']
})

export class UsersTableComponent implements OnInit {
  data: UsersVM[] = [];
  trajanje = 2;
  users!: UsersVM;
  dataSource = new MatTableDataSource(this.data);

  displayedColumns: string[] = [
    'username',
    'email',
    'action',
  ];

  constructor(private service: UsersService, private porukaSucess: MatSnackBar) { }

  ngOnInit(): void {
    this.service.GetUsers().subscribe((x: any) => {
      this.data = x;
      console.log(this.data);
      this.dataSource = new MatTableDataSource(this.data);
    })
  }

  obrisiKorisnika(user: UsersVM) {
    this.service.DeleteUsers(user).subscribe(() => {
      // this.data.forEach((value, index)=>{
      //   if(value.id==user.id){
      //     this.data.splice(index,1);
      //   }
      // })
      this.service.GetUsers().subscribe((x: any) => {
        this.data = x;
        console.log(this.data);
      })
    })
    this.porukaSucess.openFromComponent(PorukeComponent, {
      duration: this.trajanje * 1000,
    })
  }

  odaberiKorisnika(korisnik: UsersVM) {
    this.users = korisnik;
    this.users.show = true;
  }

  filterData(event: any) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.data = this.data.filter(e => e.username.toLowerCase().includes(event.target.value.toLowerCase()))
  }

}
