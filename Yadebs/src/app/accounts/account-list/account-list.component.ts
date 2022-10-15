import { Component, OnInit, Inject } from '@angular/core';
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
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { pairwise, take, filter } from 'rxjs/operators';
import { Router, Event, NavigationEnd } from '@angular/router';

@Component({
  selector: 'app-account-list',
  templateUrl: './account-list.component.html',
  styleUrls: ['./account-list.component.css'],
})
export class AccountListComponent implements OnInit {
  accounts$ = this.store.pipe(select(selectAllAccounts));
  loading$ = this.store.pipe(select(selectAccountState));
  constructor(
    private store: Store,
    private route: ActivatedRoute,
    public dialog: MatDialog,
    private router: Router
  ) {
    router.events.subscribe((val) => {
      if (val instanceof NavigationEnd) {
        console.log(val);
        if (val.url != '/accounts/list') {
          let dialogRef = this.dialog.open(AccountEditComponent);

          dialogRef
            .afterClosed()
            .subscribe(() => this.router.navigateByUrl('accounts/list'));
        }
      }
    });
  }

  ngOnInit(): void {
    this.loadAccounts();
  }
  addAccountDialog(): void {
    this.dialog.open(AccountEditComponent);
  }
  loadAccounts(): void {
    this.store.dispatch(loadAccounts());
  }
}
