import { Injectable } from '@angular/core';
import { Account } from './account';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  getAccounts(): Observable<Account[]> {
    return this.httpClient.get<Account[]>(
      'https://localhost:7211/api/Accounts'
    );
  }

  addAccount(account: Account): Observable<Account> {
    return this.httpClient.post<Account>(
      'https://localhost:7211/api/Accounts',
      account
    );
  }

  deleteAccount(id: string) {
    return this.httpClient.delete(`https://localhost:7211/api/Accounts/${id}`);
  }

  updateAccount(account: Account): Observable<Account> {
    return this.httpClient.put<Account>(
      `https://localhost:7211/api/Accounts/${account.id}`,
      account
    );
  }
  constructor(private httpClient: HttpClient) {}
}
