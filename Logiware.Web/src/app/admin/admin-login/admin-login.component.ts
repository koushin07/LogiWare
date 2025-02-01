import { Component } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthenticationService } from '../../_services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin-login',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './admin-login.component.html',
  styleUrl: './admin-login.component.scss'
})
export class AdminLoginComponent {
  loginForm!: FormGroup;

    constructor(private fb: FormBuilder, private authenticationService: AuthenticationService, private router: Router) {
      this.loginForm = fb.group({
        email: ['', [Validators.required, Validators.email]],
        password: ['', [Validators.required]]
      })
    }

    public onSubmit() {
      console.log(this.loginForm.invalid)
      if (this.loginForm.invalid) {
        return;
      }
      this.authenticationService.login(this.loginForm).subscribe(() => {
        console.log("sign in")
        this.router.navigateByUrl("admin/dashboard")
      })
    }
}
