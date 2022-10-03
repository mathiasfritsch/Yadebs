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
  constructor(private httpClient: HttpClient) {}
}
