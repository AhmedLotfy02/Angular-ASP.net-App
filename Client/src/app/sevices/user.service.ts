import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { UserData } from '../models/userData.model';
import { resetData } from '../models/resetData.model';
 
@Injectable({
  providedIn: 'root'
})
export class UserService {
 
 
  private addnewUserListener= new Subject<boolean>();
  private retrievedUserListener=new Subject<{user:UserData,status:boolean}>();
  getretrievedUserListener(){
    return this.retrievedUserListener.asObservable();
  }
  getnewUserListener(){
    return this.addnewUserListener.asObservable();
  }
  constructor(private http: HttpClient) {
  }
 
 
  add_User_Profile(
    user:UserData
  ) {
   
    this.http.post('http://localhost:5000/api/AddProfile', user).subscribe(
      (response) => {
       console.log(response);
       if(response=="Duplicate Username")
       this.addnewUserListener.next(false);
       else
       this.addnewUserListener.next(true);
      },
      (error) => {
        this.addnewUserListener.next(false);
      }
    );

  }

  retrieveData(data:resetData){
    
  
    this.http.post<any>('http://localhost:5000/api/RetrieveUserData', data).subscribe(
      (response:any) => {
       console.log(response);
       if(response=="username not found"||response=="Wrong Password"){
          this.retrievedUserListener.next({"user":response[0],"status":false});
          
       }
       else{
       this.retrievedUserListener.next({"user":response,"status":true});
          console.log(response);
       }
      },
      (error) => {
        this.addnewUserListener.next(false);
      }
    );

  }

  

  changePassword(resetdata:resetData ){
    this.http.post('http://localhost:5000/api/ResetPassword', resetdata).subscribe(
      (response) => {
       console.log(response);
       if(response=="Wrong Password"||response == "username not found")
       this.addnewUserListener.next(false);
       else
       this.addnewUserListener.next(true);
      },
      (error) => {
        this.addnewUserListener.next(false);
      }
    );


  }
 
}
 