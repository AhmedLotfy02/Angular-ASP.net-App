import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Names } from 'src/app/models/names.model';
import { UserData } from 'src/app/models/userData.model';
import { UserPersonalData } from 'src/app/models/UserPersonalData.model';
import { UserService } from 'src/app/sevices/user.service';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateComponent implements OnInit {
  private status!:Subscription;
   changed=false;
   duplicate=false;
  submitted = false;
  form: FormGroup = new FormGroup({
    firstname: new FormControl(''),
    fathername: new FormControl(''),
    familyname: new FormControl(''),
    birthdate:new FormControl(''),
    occupation:new FormControl(''),
    address: new FormControl(''),
    username: new FormControl(''),
    password: new FormControl('')
    });

  constructor(private formBuilder: FormBuilder,private service:UserService) {
  }

  ngOnInit(): void {
    this.status = this.service
      .getnewUserListener()
      .subscribe((ischanged) => {
        if(ischanged){
          this.duplicate=false;
          this.changed = ischanged;

        }
        else
        {
          this.duplicate=true;
          this.changed=false;
        }
      });
    this.form = this.formBuilder.group(
      {
        firstname: ['', Validators.required],
        fathername: ['', Validators.required],
        familyname: ['', Validators.required],
        birthdate: ['', Validators.required],
        occupation: ['', Validators.required],
        address: ['', Validators.required],

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
    const personaldata:UserPersonalData={
      "birthdate":this.form.value.birthdate,
      "address":this.form.value.address,
      "occupation":this.form.value.occupation
    }
    const nameData:Names={
      "FirstName":this.form.value.firstname,
      "FatherName":this.form.value.fathername,
      "FamilyName":this.form.value.familyname
    }
    const userData:UserData={
      "UserPersonalData":personaldata,
      "nameData":nameData,
      "username":this.form.value.username,
      "password":this.form.value.password
    }
    console.log("hi1");
    this.service.add_User_Profile(userData);
    //console.log(JSON.stringify(this.form.value, null, 2));

  }
  onReset(): void {
    this.submitted = false;
    this.form.reset();
  }
}