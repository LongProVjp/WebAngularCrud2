import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ApiServiceService {
  readonly apiUrl="https://localhost:7005/api/"
  constructor(private http:HttpClient) { }
//Province
  public getProvinceList(): Observable<any> {
    return this.http.get<any>(this.apiUrl + 'Province');
  }

  getProvincebyName(val?:string):Observable<any>{
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.get<any[]> (this.apiUrl+'Province/GetbyName/'+val,httpOptions);
  }

  GetProvinceID(val:number):Observable<any>{
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.get<any[]> (this.apiUrl+'Province/'+val,httpOptions)
  }

  postProvince(val:any){
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.post(this.apiUrl+'Province',val,httpOptions)
  }

  putProvince(val:any,id?:number){
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.put(this.apiUrl+'Province/'+id,val,httpOptions)
  }

  deleteProvince(id:number){
    const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    return this.http.delete(this.apiUrl+'Province/'+id,httpOptions)
  }
//District

getDistrict():Observable<any>{
  return this.http.get<any[]> (this.apiUrl+'District');
}

getFullProvinceList():Observable<any>{
  return this.http.get<any[]> (this.apiUrl+'District/withProvince');
}

postDistrict(val:any){
  const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
  return this.http.post(this.apiUrl+'District',val,httpOptions)
}

putDistrict(val:any,id?:number){
  const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
  return this.http.put(this.apiUrl+'District/'+id,val,httpOptions)
}

deleteDistrict(id:number){
  const httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
  return this.http.delete<number>(this.apiUrl+'District/'+id,httpOptions)
}

getAllDepartmentNames(): Observable<any[]> {
  return this.http.get<any[]>(this.apiUrl + 'District/GetAllProvince');
}

}
