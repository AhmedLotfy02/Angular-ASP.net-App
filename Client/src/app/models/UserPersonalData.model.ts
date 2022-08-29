export class UserPersonalData {
    birthdate: string;
    occupation: string;
    address:string;
  
    constructor( birthdate: string,occupation: string,address: string ) {
      this.birthdate = birthdate;
     this.occupation=occupation;
     this.address=address;
    }
}