import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { resetData } from 'src/app/models/resetData.model';
import { UserData } from 'src/app/models/userData.model';
import { UserService } from 'src/app/sevices/user.service';

@Component({
  selector: 'app-update',
  templateUrl: './update.component.html',
  styleUrls: ['./update.component.css']
})
export class UpdateComponent implements OnInit {
  submitted = false;
  private status!:Subscription;
  failed=false;
  appear=false;
  retrievedData!:any;
  form: FormGroup = new FormGroup({
   
    username: new FormControl(''),
    password: new FormControl('')
    });
   
  constructor(private formBuilder: FormBuilder,private service:UserService) {}

  ngOnInit(): void {
    this.status = this.service
    .getretrievedUserListener()
    .subscribe((response:any) => {
      if(response.status==true){
        console.log("in true retrun");
      this.retrievedData=response.user;
      console.log(this.retrievedData);
      setTimeout(() => {
        this.appear=true;  
      }, 1000);
      
      }
      else
       { this.failed=true;}
    });
    this.form = this.formBuilder.group(
      {
     
        username: [
          '',
          [
            Validators.required
          ]
        ],
        password: [
          '',
          [
            Validators.required,
            Validators.minLength(8),
            Validators.pattern(/(?=.*\d)(?=.*[a-z])(?=.*[A-Z])/)
          ]
        ]
      }
    );
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }
  onSubmit(): void {

    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    console.log(this.form.value);
    const data:resetData={
      "username":this.form.value.username,
      "oldPassword":this.form.value.password,
      "newPassword":"   "
    }
    this.service.retrieveData(data);   
  }
  onReset(): void {
    this.submitted = false;
    this.form.reset();
  }
}