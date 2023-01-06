import { Component, OnInit } from '@angular/core';
export interface DialogData {
  animal: string;
  name: string;
}

import {
  MatDialog,
  MAT_DIALOG_DATA,
  MatDialogRef,
} from '@angular/material/dialog';
import { select, Store } from '@ngrx/store';
import { Subject, takeUntil } from 'rxjs';
import { loadAccounts } from '../accounts/store/account.actions';
import { selectAllAccounts } from '../accounts/store/account.selectors';
import { Account } from '../shared/account';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  accounts: Account[] = [];
  private ngUnsubscribe = new Subject<void>();
  constructor(private store: Store) {
    this.store
      .pipe(select(selectAllAccounts), takeUntil(this.ngUnsubscribe))
      .subscribe((accounts) => {
        this.accounts = accounts;
      });
  }
  ngOnInit(): void {
    this.store.dispatch(loadAccounts());
  }
}
