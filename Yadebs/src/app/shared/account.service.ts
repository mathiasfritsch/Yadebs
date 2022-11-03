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
  constructor(private httpClient: HttpClient) {}
}
