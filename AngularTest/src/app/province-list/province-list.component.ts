import { Component, OnInit } from '@angular/core';
import { ApiServiceService } from '../api-service.service';

@Component({
  selector: 'app-province-list',
  templateUrl: './province-list.component.html',
  styleUrls: ['./province-list.component.css']
})
export class ProvinceListComponent implements OnInit {

  constructor(private apiservice:ApiServiceService) { }
  id?:number;
  provin: any;
  ProvinceList: any=[];
  isVisible = false;
  isVisible2 = false;
  provinceid?:number;
  provincename?: string;
  ngOnInit(): void {
    this.refreshList();
  }
  refreshList() {
    this.apiservice.getProvinceList().subscribe(data => {
      this.ProvinceList = data;
    });
  }
    AddClick(): void {
      var pro = { id:this.provinceid,ProvinceName: this.provincename };
      this.apiservice.postProvince(pro).subscribe(any => {
        this.refreshList();
      });
      this.isVisible = false;
  }
  deleteClick(item: any) {
      this.apiservice.deleteProvince(item.id).subscribe(data => {
        this.refreshList();
      })
  }

  UpdateClick(){    
    var pro = {id:this.id,ProvinceName:this.provincename };
    this.apiservice.putProvince(pro,this.id).subscribe(any=> {this.refreshList()} );
    this.isVisible2 = false;
  }

  showModal(): void {
    this.isVisible = true;
    this.provinceid = 0;
    this.provincename =null!;
  }

  showModal2(id:number,ProvinceName:string): void {
    this.isVisible2 = true;
    this.id = id;
    this.provincename =ProvinceName;
    
  }

  CancelClick(): void {
    this.isVisible = false;
    this.provinceid=0;
  }
  CancelClick2(): void {
    this.isVisible2 = false;
    this.provinceid=0;
  }
}
