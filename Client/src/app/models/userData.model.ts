import { Names } from "./names.model";
import { UserPersonalData } from "./UserPersonalData.model";

export class UserData {
    
     nameData:Names;
     UserPersonalData:UserPersonalData;
     username:string;
     password:string;
    
  
    constructor( nameData:Names,UserPersonalData:UserPersonalData,username:string,password:string) {
      this.nameData=nameData;
      this.UserPersonalData=UserPersonalData;
      this.username=username;
      this.password=password;
    }
}