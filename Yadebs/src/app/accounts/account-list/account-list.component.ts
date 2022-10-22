import { Component, OnInit, Inject } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { loadAccounts } from '../store/account.actions';
import {
  selectAccountState,
  selectAllAccounts,
  selectEntity,
} from '../store/account.selectors';
import { ActivatedRoute } from '@angular/router';
import {
  AccountEditComponent,
  openEditCourseDialog,
} from '../account-edit/account-edit.component';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router, NavigationEnd, Event, NavigationStart } from '@angular/router';
import { Observable, tap, switchMap, filter } from 'rxjs';
import { Account } from 'src/app/shared/account';

import { MatTreeNestedDataSource } from '@angular/material/tree';
import { FlatTreeControl, NestedTreeControl } from '@angular/cdk/tree';

import {
  MatTreeFlatDataSource,
  MatTreeFlattener,
} from '@angular/material/tree';

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
    router.events
      .pipe(
        filter(
          (e: Event): e is NavigationEnd =>
            e instanceof NavigationEnd &&
            e.url != '/accounts/list' &&
            e.url != '/accounts/list/0'
        ),
        switchMap((re) =>
          this.store.pipe(
            select(selectEntity(Number(re.url.charAt(re.url.length - 1))))
          )
        )
      )
      .subscribe((accountSelected) => {
        // let dialogRef = this.dialog.open(AccountEditComponent, {
        //   data: {
        //     account: accountSelected,
        //   },
        // });
        // dialogRef
        //   .afterClosed()
        //   .subscribe(() => this.router.navigateByUrl('accounts/list'));

        if (accountSelected)
          openEditCourseDialog(this.dialog, accountSelected)
            .pipe(filter((val) => !!val))
            .subscribe((val) => console.log('new course value:', val));
      });
  }

  ngOnInit(): void {
    this.loadAccounts();
  }
  addAccountDialog(): void {
    this.router.navigateByUrl('accounts/list/0');
    let dialogRef = this.dialog.open(AccountEditComponent, {
      data: { account: { id: 0, name: '', number: 0 } },
    });
    dialogRef
      .afterClosed()
      .subscribe(() => this.router.navigateByUrl('accounts/list'));
  }
  loadAccounts(): void {
    this.store.dispatch(loadAccounts());
  }
}
