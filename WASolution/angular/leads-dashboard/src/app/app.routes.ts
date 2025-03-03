import { Routes } from '@angular/router';
import { LeadsListComponent } from './features/leads/leads-list/leads-list.component';
import { LeadsFormComponent } from './features/leads/leads-form/leads-form.component';

export const routes: Routes = [
  { path: '', redirectTo: 'leads', pathMatch: 'full' },
  { path: 'leads', component: LeadsListComponent },
  { path: 'leads/new', component: LeadsFormComponent } 
];
