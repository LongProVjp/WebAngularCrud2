import { Component, OnInit } from '@angular/core';
import { ApiServiceService } from '../api-service.service';

@Component({
  selector: 'app-province',
  templateUrl: './province.component.html',
  styleUrls: ['./province.component.css']
})
export class ProvinceComponent implements OnInit {

  constructor(private apiservice:ApiServiceService) { }

  ProvinceFullList: any[] = []
  id?:number
  provinceid?:number
  provincename?: string;
  districtname?: string;
  isVisible = false;
  isVisible2 = false;
  searchValue = '';
  visible = false;
  ProvinceList:any[]=[]

  ngOnInit(): void {
    this.refreshProvinceList();
  }

  refreshProvinceList() {
    this.apiservice.getAllDepartmentNames().subscribe((data: any) => {
      this.ProvinceList = data;
    });
    this.apiservice.getFullProvinceList().subscribe(data => { this.ProvinceFullList = data,this.search()})
  }



  AddClick(): void {
    var pro = { id:this.provinceid,ProvinceName: this.provincename };
    this.apiservice.postProvince(pro).subscribe(any =>{      
      this.apiservice.getProvincebyName(this.provincename).subscribe(data=> {
        var dis = { id:0,DistrictName: this.districtname,ProvinceId: data.id };
        this.apiservice.postDistrict(dis).subscribe(()=> {
          this.refreshProvinceList()} )
      })
    } );
    this.isVisible = false;
  }
  UpdateClick(){    
    this.apiservice.getProvincebyName(this.provincename).subscribe(data=> {
      var dis = { id:this.id,DistrictName: this.districtname,ProvinceId:this.provinceid };
      this.apiservice.putDistrict(dis,this.id).subscribe(()=> {this.refreshProvinceList()} );
    })
    this.isVisible2 = false;
  }

  DeleteClick(item: any) {
    this.apiservice.deleteDistrict(item.id).subscribe(data => {
      this.refreshProvinceList();
    })
}
  showModal(): void {
    this.isVisible = true;
    this.provincename =null!;
    this.districtname =null!;
    this.id=0;
  }
  showModal2(id:number,DistrictName:string,ProvinceName:string,ProvinceId:number): void {
    this.isVisible2 = true;
    this.id=id;
    this.provincename =ProvinceName;
    this.districtname =DistrictName;
    this.provinceid=ProvinceId;

  }
  search(): void {
    this.visible = false;
    this.ProvinceFullList = this.ProvinceFullList.filter((item: any) => item.provinceName.indexOf(this.searchValue) !== -1);
  }

  CancelClick(): void {
    this.isVisible = false;
    this.provinceid=0;
  }

  CancelClick2(): void {
    this.isVisible2 = false;
    this.provinceid=0;
  }
  reset(): void {
    this.searchValue = '';
    this.refreshProvinceList();
    }
}
