import { Component, OnInit } from '@angular/core';
import { LeadService } from '../leads.service';
import { Lead } from '../../../models/lead.model';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { LeadDetailsDialogComponent } from '../lead-details-dialog/lead-details-dialog.component';

@Component({
  selector: 'app-leads-list',
  templateUrl: './leads-list.component.html',
  styleUrls: ['./leads-list.component.scss'],
  standalone: true,
  imports: [MatTableModule, MatButtonModule, MatDialogModule]
})
export class LeadsListComponent implements OnInit {
  leads: Lead[] = [];
  displayedColumns: string[] = ['name', 'phone', 'zip', 'consent', 'actions'];

  constructor(private leadService: LeadService, private dialog: MatDialog) {}

  ngOnInit(): void {
    this.leadService.getLeads().subscribe((data) => (this.leads = data));
  }

  openLeadDetails(lead: Lead) {
    this.dialog.open(LeadDetailsDialogComponent, {
      width: '400px',
      data: lead
    });
  }
}
