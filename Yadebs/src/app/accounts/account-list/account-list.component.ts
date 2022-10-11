import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { Account } from 'src/app/account.model';
import { loadAccounts } from '../store/account.actions';
import {
  selectAccountState,
  selectAllAccounts,
} from '../store/account.selectors';
import { Observable } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { AccountEditComponent } from '../account-edit/account-edit.component';
@Component({
  selector: 'app-account-list',
  templateUrl: './account-list.component.html',
  styleUrls: ['./account-list.component.css'],
})
export class AccountListComponent implements OnInit {
  accounts$ = this.store.pipe(select(selectAllAccounts));
  loading$ = this.store.pipe(select(selectAccountState));
  constructor(private store: Store, private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.loadAccounts();
  }

  loadAccounts(): void {
    this.store.dispatch(loadAccounts());
  }
}
