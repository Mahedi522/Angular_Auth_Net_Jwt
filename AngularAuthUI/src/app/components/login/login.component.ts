import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import ValidateForm from 'src/app/helper/validateform';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  type: string = "password"
  isText: boolean = false;
  eyeIcon: string = "fa-eye-slash";
  loginForm! : FormGroup;

  constructor(private fb: FormBuilder, private auth: AuthService){  }

  ngOnInit(): void{
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    })
  }
  hideShowPass(){
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = "fa-eye" : this.eyeIcon = "fa-eye-slash";
    this.isText ? this.type = "text" : this.type = "password";
  }
  onLogin(){
    if (this.loginForm.valid) {
      //send object to database
      console.log(this.loginForm.value);

      this.auth.login(this.loginForm.value).subscribe({next:(res)=>{alert(res.message)}, error: (err)=>{alert(err?.error.message)}})
    }
    else{
      //throw the error using toaster and with required fields
      this.validateAllFormFields(this.loginForm);
      alert("your form is invalid")
    }
  }
  private validateAllFormFields(formGroup:FormGroup){
    Object.keys(formGroup.controls).forEach(field=>{
      const control = formGroup.get(field);
      if(control instanceof FormControl){
        control.markAsDirty({onlySelf:true});
      }
      else if (control instanceof FormGroup) {
        ValidateForm.validateAllFormFields(control)
      }
    })
  }
}