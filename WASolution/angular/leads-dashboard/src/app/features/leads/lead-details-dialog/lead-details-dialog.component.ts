import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogModule } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { Lead } from '../../../models/lead.model';

@Component({
  selector: 'app-lead-details-dialog',
  templateUrl: './lead-details-dialog.component.html',
  styleUrls: ['./lead-details-dialog.component.scss'],
  standalone: true,
  imports: [CommonModule, MatDialogModule]
})
export class LeadDetailsDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<LeadDetailsDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public lead: Lead
  ) {}

  close(): void {
    this.dialogRef.close();
  }
}
