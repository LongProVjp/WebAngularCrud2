import { Component,OnInit } from '@angular/core';
import { ApiServiceService } from './api-service.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor(private apiservice:ApiServiceService) { }
  Login =false;
  PasswordVisible=false;
  Username?: string;
  Password?: string;

  ngOnInit(): void { }

  LoginClick(){
    var login = {Username:this.Username,Password:this.Password}
    this.apiservice.postUserLogin(login).subscribe(data => {
      localStorage.setItem('token',data.token);
      this.Login =true;
      this.Username='';
      this.Password='';
    })
  }
  LogoutClick(){
    localStorage.setItem('token','');
    this.Login =false;
    }

  LoginAddClick(){
    var add={Username:this.Username,Password:this.Password}
    this.apiservice.postUserAdd(add).subscribe(res => alert(res.toString()));
      
    }
}
