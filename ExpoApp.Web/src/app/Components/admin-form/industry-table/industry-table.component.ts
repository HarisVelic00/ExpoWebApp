import { Component, OnInit } from '@angular/core';
import { IndustryVM } from '../../../models/IndustryVM';
import { IndustryService } from '../../../services/industry/industry.service';
import { PorukeComponent } from '../../poruke/poruke.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-industry-table',
  templateUrl: './industry-table.component.html',
  styleUrls: ['./industry-table.component.css']
})

export class IndustryTableComponent implements OnInit {
  data: IndustryVM[] = [];
  trajanje = 3;
  industry!: IndustryVM;
  dataSource = new MatTableDataSource(this.data);

  displayedColumns: string[] = [
    'name',
    'action',
  ];

  constructor(private service: IndustryService, private porukaSucess: MatSnackBar) { }

  ngOnInit(): void {
    this.service.GetIndustry().subscribe((x: any) => {
      this.data = x;
      console.log(this.data);
      this.dataSource = new MatTableDataSource(this.data);
    })
  }

  obrisiIndustry(element: IndustryVM) {
    this.service.DeleteIndustry(element).subscribe(() => {
      // this.data.forEach((value, index) => {
      //   if (value.id == element.id) {
      //     this.data.splice(index, 1);
      //   }
      // })
      this.service.GetIndustry().subscribe((res: any) => {
        this.data = res;
        console.log(this.data);
      })
    })
    this.porukaSucess.openFromComponent(PorukeComponent, {
      duration: this.trajanje * 1000,
    })
  }

  odaberiIndustriju(industrija: IndustryVM) {
    this.industry = industrija;
    this.industry.prikazi = true;
    console.log(this.industry);
  }

  filterData(event: any) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.data = this.data.filter(e => e.name.toLowerCase().includes(event.target.value.toLowerCase()))
  }

}
