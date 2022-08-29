export class resetData {
    username: string;
    oldPassword: string;
    newPassword:string;
  
    constructor( username: string,oldPassword: string,newPassword: string ) {
      this.username = username;
     this.oldPassword=oldPassword;
     this.newPassword=newPassword;
    }
}