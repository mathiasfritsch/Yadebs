import { Injectable } from '@angular/core';
import { Journal } from './journal';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class JournalService {
  getJournals(): Observable<Journal[]> {
    return this.httpClient.get<Journal[]>('https://localhost:7211/api/Journal');
  }

  addJournal(journal: Journal): Observable<Journal> {
    return this.httpClient.post<Journal>(
      'https://localhost:7211/api/Journal',
      journal
    );
  }

  deleteJournal(id: string) {
    return this.httpClient.delete(`https://localhost:7211/api/Journal/${id}`);
  }

  updateJournal(journal: Journal): Observable<Journal> {
    return this.httpClient.put<Journal>(
      `https://localhost:7211/api/Journal/${journal.id}`,
      journal
    );
  }
  constructor(private httpClient: HttpClient) {}
}
