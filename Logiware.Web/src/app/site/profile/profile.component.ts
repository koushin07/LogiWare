import { Component, inject, OnInit, signal } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

import { HlmLabelDirective } from '@spartan-ng/ui-label-helm';

import { HlmInputDirective } from '@spartan-ng/ui-input-helm';
import { Site } from '../../_model/site';
import { User } from '../../_model/user';
import { PersonnelService } from '../../_services/personnel.service';
import { CreatePersonnel, Personnel } from '../../_model/personnel';
import { CommonModule } from '@angular/common';
import { Authenticated } from '../../_model/authenticated';

import { HlmButtonDirective } from '@spartan-ng/ui-button-helm';
import {
  BrnDialogContentDirective,
  BrnDialogTriggerDirective
} from "@spartan-ng/ui-dialog-brain";
import {
  HlmDialogComponent,
  HlmDialogContentComponent,
  HlmDialogDescriptionDirective,
  HlmDialogFooterComponent,
  HlmDialogHeaderComponent,
  HlmDialogTitleDirective,
} from '@spartan-ng/ui-dialog-helm';

import { HlmFormFieldModule } from '@spartan-ng/ui-formfield-helm';
import { BrnSelectImports, BrnSelectModule } from "@spartan-ng/ui-select-brain";
import { HlmSelectImports, HlmSelectModule } from "@spartan-ng/ui-select-helm"
import { AuthenticationService } from '../../_services/authentication.service';
@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [
    BrnSelectImports,
    HlmSelectImports,
    HlmSelectModule,
    BrnSelectModule,
    HlmFormFieldModule,
    ReactiveFormsModule,
    HlmDialogComponent,
    HlmDialogContentComponent,
    HlmDialogDescriptionDirective,
    HlmDialogFooterComponent,
    HlmDialogHeaderComponent,
    HlmDialogTitleDirective,
    FormsModule,
    HlmInputDirective,
    HlmLabelDirective, CommonModule, HlmButtonDirective, BrnDialogContentDirective, BrnDialogTriggerDirective],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent {
  addingPersonnel: boolean = false;
  user: User | null = null;
  siteName: string = '';
  siteEmail: string = '';
  currentPassword: string = '';
  newPassword: string = '';
  confirmPassword: string = '';
  personnels = signal<Personnel[]>([])
  personnelForm!: FormGroup;

  constructor(private personnelService: PersonnelService, private fb: FormBuilder, private authService: AuthenticationService) {
    this.personnelForm = fb.group({
      firstName: ['', [Validators.required]],
      lastName: ['', Validators.required],
      role: ['', Validators.required],
      siteId: authService.user()?.site.id

    })
    const item = localStorage.getItem('auth')
    if (item) {
      const itemUser = JSON.parse(item) as Authenticated
      this.user = itemUser.user
      this.personnelService.getPersonnelBySite(this.user.site.id).subscribe(data => {
        this.personnels.set(data)
      })
    }

  }



  changePassword() {
    if (this.newPassword !== this.confirmPassword) {
      alert('New passwords do not match!');
      return;
    }
    // Implement password change logic here
    console.log('Password change requested');
  }

  addPersonnel(ctx: any) {

    // Implement add personnel logic here
    if (this.personnelForm.valid) {
      const newPersonnel: CreatePersonnel = this.personnelForm.value;
      newPersonnel.siteId = this.user?.site.id!;
      this.personnelService.createPersonnel(newPersonnel).subscribe(data => {
        this.personnels.set([...this.personnels(), data]);
        this.personnelForm.reset();
        ctx.close();
      })
    } else {
      alert('Please fill out all required fields.');
    }
    console.log('Add personnel clicked');
  }

  editPersonnel(person: Personnel) {
    // Implement edit personnel logic here
    console.log('Edit personnel:', person);
  }

  deletePersonnel(person: Personnel) {
    // Implement delete personnel logic here
    console.log('Delete personnel:', person);
  }
}
