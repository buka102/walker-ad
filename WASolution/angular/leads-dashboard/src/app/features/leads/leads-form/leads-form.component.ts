import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { LeadService } from '../leads.service';
import { Router } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-leads-form',
  templateUrl: './leads-form.component.html',
  styleUrls: ['./leads-form.component.scss'],
  standalone: true,
  imports: [
    ReactiveFormsModule, 
    MatFormFieldModule, 
    MatInputModule, 
    MatCheckboxModule, 
    MatButtonModule
  ]
})
export class LeadsFormComponent {
  leadForm: FormGroup;

  constructor(private fb: FormBuilder, private leadService: LeadService, private router: Router) {
    this.leadForm = this.fb.group({
      name: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      zipCode: ['', Validators.required],
      consentToContact: [false, Validators.required],
      email: ['']
    });
  }

  submitLead() {
    if (this.leadForm.valid) {
      this.leadService.createLead(this.leadForm.value).subscribe(() => {
        alert('Lead submitted successfully!');
        this.router.navigate(['/leads']); 
      });
    }
  }
}
