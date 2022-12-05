import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ApiServiceService {
  readonly apiUrl="https://localhost:7005/api/"
  constructor(private http:HttpClient) { }

  getHeader(){
    var token = localStorage.getItem('token')
    return token? new HttpHeaders().set('Authorization', 'Bearer '+token): null;
  }
  postUserAdd(val:any):Observable<any>{
    return this.http.post(this.apiUrl+'User/Add',val)
  }
  postUserLogin(val:any):Observable<any>{
    return this.http.post(this.apiUrl+'User',val)
  }
//Province
  public getProvinceList(): Observable<any> {
    let headers = this.getHeader();
    if (headers instanceof HttpHeaders)
    return this.http.get<any>(this.apiUrl + 'Province',{headers: headers})
    return this.http.get<any>(this.apiUrl + 'Province');
  }

  getProvincebyName(val?:string):Observable<any>{
    let headers = this.getHeader();
    if (headers instanceof HttpHeaders)
    return this.http.get<any[]> (this.apiUrl+'Province/GetbyName/'+val,{headers: headers})
    return this.http.get<any[]> (this.apiUrl+'Province/GetbyName/'+val);
  }

  GetProvinceID(val:number):Observable<any>{
    let headers = this.getHeader();
    if (headers instanceof HttpHeaders)
    return this.http.get<any[]> (this.apiUrl+'Province/'+val,{headers: headers})
    return this.http.get<any[]> (this.apiUrl+'Province/'+val)
  }

  postProvince(val:any){
    let headers = this.getHeader();
    if (headers instanceof HttpHeaders)
    return this.http.post(this.apiUrl+'Province',val,{headers: headers})
    return this.http.post(this.apiUrl+'Province',val)
  }

  putProvince(val:any,id?:number){
    let headers = this.getHeader();
    if (headers instanceof HttpHeaders)
    return this.http.put(this.apiUrl+'Province/'+id,val,{headers: headers})
    return this.http.put(this.apiUrl+'Province/'+id,val)
  }

  deleteProvince(id:number){
    let headers = this.getHeader();
    if (headers instanceof HttpHeaders)
    return this.http.delete(this.apiUrl+'Province/'+id,{headers: headers})
    return this.http.delete(this.apiUrl+'Province/'+id)
  }
//District

getDistrict():Observable<any>{
  let headers = this.getHeader();
    if (headers instanceof HttpHeaders)
    return this.http.get<any[]> (this.apiUrl+'District',{headers: headers})
  return this.http.get<any[]> (this.apiUrl+'District');
}

getFullProvinceList():Observable<any>{
  let headers = this.getHeader();
  if (headers instanceof HttpHeaders)
  return this.http.get<any[]> (this.apiUrl+'District/withProvince',{headers: headers})
  return this.http.get<any[]> (this.apiUrl+'District/withProvince');
}

postDistrict(val:any){
  let headers = this.getHeader();
  if (headers instanceof HttpHeaders)
  return this.http.post(this.apiUrl+'District',val,{headers: headers})
  return this.http.post(this.apiUrl+'District',val)
}

putDistrict(val:any,id?:number){
  let headers = this.getHeader();
  if (headers instanceof HttpHeaders)
  return this.http.put(this.apiUrl+'District/'+id,val,{headers: headers})
  return this.http.put(this.apiUrl+'District/'+id,val)
}

deleteDistrict(id:number){
  let headers = this.getHeader();
  if (headers instanceof HttpHeaders)
  return this.http.delete<number>(this.apiUrl+'District/'+id,{headers: headers})
  return this.http.delete<number>(this.apiUrl+'District/'+id)
}

getAllDepartmentNames(): Observable<any[]> {
  let headers = this.getHeader();
  if (headers instanceof HttpHeaders)
  return this.http.get<any[]>(this.apiUrl + 'District/GetAllProvince',{headers: headers})
  return this.http.get<any[]>(this.apiUrl + 'District/GetAllProvince');
}

}
