import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Lead } from '../../models/lead.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LeadService {

  private apiUrl = `${environment.apiUrl}/leads`; 

  constructor(private http: HttpClient) {}

  getLeads(): Observable<Lead[]> {
    return this.http.get<Lead[]>(this.apiUrl);
  }

  createLead(lead: Lead): Observable<Lead> {
    return this.http.post<Lead>(this.apiUrl, lead);
  }
}
