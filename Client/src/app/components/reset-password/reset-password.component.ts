import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { Names } from 'src/app/models/names.model';
import { resetData } from 'src/app/models/resetData.model';
import { UserData } from 'src/app/models/userData.model';
import { UserPersonalData } from 'src/app/models/UserPersonalData.model';
import { UserService } from 'src/app/sevices/user.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {

  private status!:Subscription;
   changed=false;
   duplicate=false;
  submitted = false;
  form: FormGroup = new FormGroup({
    username: new FormControl(''),
    oldPassword: new FormControl(''),
    newPassword: new FormControl('')
    });

  constructor(private formBuilder: FormBuilder,private service:UserService) {
  }

  ngOnInit(): void {
    this.status = this.service
      .getnewUserListener()
      .subscribe((ischanged) => {
        if(ischanged){
        this.changed = ischanged;
          this.duplicate=false;
      }
        else{
          this.duplicate=true;
          this.changed=false;
        }
          
      });
    this.form = this.formBuilder.group(
      {
        username: [
          '',
          [
            Validators.required
          ]
        ],
        oldPassword: [
          '',
          [
            Validators.required,
            Validators.minLength(8),
            Validators.pattern(/(?=.*\d)(?=.*[a-z])(?=.*[A-Z])/)
          ]
        ],
        newPassword: [
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
    console.log("asdad");
    console.log(this.form.value);
    const resetdata:resetData={
      "username":this.form.value.username,
      "oldPassword":this.form.value.oldPassword,
      "newPassword":this.form.value.newPassword
    }
    this.service.changePassword(resetdata);
    //console.log(JSON.stringify(this.form.value, null, 2));

  }
  onReset(): void {
    this.submitted = false;
    this.form.reset();
  }

}
