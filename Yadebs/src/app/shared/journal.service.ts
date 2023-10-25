import { Injectable } from '@angular/core';
import { Journal } from './journal';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class JournalService {
  getJournals(): Observable<Journal[]> {
    return this.httpClient.get<Journal[]>(
      'https://localhost:7211/api/Journals'
    );
  }

  addJournal(journal: Journal): Observable<Journal> {
    return this.httpClient.post<Journal>(
      'https://localhost:7211/api/Journals',
      journal
    );
  }

  deleteJournal(id: string) {
    return this.httpClient.delete(`https://localhost:7211/api/Journals/${id}`);
  }

  updateJournal(journal: Journal): Observable<Journal> {
    return this.httpClient.put<Journal>(
      `https://localhost:7211/api/Journals/${journal.id}`,
      {
        id: journal.id,
        description: journal.description,
        transactions: [
          {
            id: journal.transactions[0].id,
            amount: journal.transactions[0].amount,
            accountId: journal.transactions[0].accountId,
          },
          {
            id: journal.transactions[1].id,
            amount: journal.transactions[1].amount,
            accountId: journal.transactions[1].accountId,
          },
        ],
      }
    );
  }
  constructor(private httpClient: HttpClient) {}
}
