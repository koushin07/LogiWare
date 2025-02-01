import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { HlmButtonDirective } from '@spartan-ng/ui-button-helm';
import { AuthenticationService } from "../../_services/authentication.service";
import { Router } from "@angular/router";
@Component({
  selector: 'app-login',
  standalone: true,
  imports: [HlmButtonDirective, ReactiveFormsModule,],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  loginForm!: FormGroup;

  constructor(private fb: FormBuilder, private authenticationService: AuthenticationService, private router: Router) {
    this.loginForm = fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(8)]]
    })
  }

  public onSubmit() {
    console.log(this.loginForm.invalid)
    if (this.loginForm.invalid) {
      return;
    }
    this.authenticationService.login(this.loginForm).subscribe(() => {
      console.log("sign in")
      this.router.navigateByUrl("/home")
    })
  }
}
