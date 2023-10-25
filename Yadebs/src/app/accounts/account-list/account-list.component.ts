import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Store, select } from '@ngrx/store';

import {
  selectAccountState,
  selectAccountTree,
  selectAllAccounts,
} from '../../store/account/account.selectors';
import { openEditAccountDialog } from '../account-edit/account-edit.component';
import { MatDialog } from '@angular/material/dialog';
import { Router, NavigationEnd, Event } from '@angular/router';
import { switchMap, filter, Subject, map, takeUntil } from 'rxjs';
import { FlatTreeControl } from '@angular/cdk/tree';
import {
  MatTreeFlatDataSource,
  MatTreeFlattener,
} from '@angular/material/tree';
import { Account } from 'src/app/shared/account';
import { loadAccounts } from '../../store/account/account.actions';
interface AccountNode {
  name: string;
  id: number;
  number: number;
  children?: AccountNode[];
}

interface AcccountFlatNode {
  expandable: boolean;
  name: string;
  level: number;
  id: number;
  number: number;
}

@Component({
  selector: 'app-account-list',
  templateUrl: './account-list.component.html',
  styleUrls: ['./account-list.component.scss'],
})
export class AccountListComponent implements OnInit, OnDestroy {
  private _transformer = (node: AccountNode, level: number) => {
    return {
      expandable: !!node.children && node.children.length > 0,
      name: node.name,
      level: level,
      id: node.id,
      number: node.number,
    };
  };
  private ngUnsubscribe = new Subject<void>();
  treeControl = new FlatTreeControl<AcccountFlatNode>(
    node => node.level,
    node => node.expandable
  );

  treeFlattener = new MatTreeFlattener(
    this._transformer,
    node => node.level,
    node => node.expandable,
    node => node.children
  );
  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);
  accounts: Account[] = [];
  loading$ = this.store.pipe(select(selectAccountState));
  hasChild = (_: number, node: AcccountFlatNode) => node.expandable;

  constructor(
    private store: Store,
    public dialog: MatDialog,
    private router: Router
  ) {
    this.store
      .pipe(select(selectAccountTree), takeUntil(this.ngUnsubscribe))
      .subscribe(accountsAsTree => {
        this.dataSource.data = accountsAsTree;
        this.treeControl.expandAll();
      });

    this.store
      .pipe(select(selectAllAccounts), takeUntil(this.ngUnsubscribe))
      .subscribe(accounts => {
        this.accounts = accounts;
      });

    this.dataSource.data = [];

    router.events
      .pipe(
        filter(
          (e: Event): e is NavigationEnd =>
            e instanceof NavigationEnd &&
            e.url != '/accounts/list' &&
            e.url != '/accounts/list/0'
        ),
        filter(() => this.accounts.length > 0),
        map(ne => Number(ne.url.split('/')[3])),
        map(id => {
          return this.accounts.find(a => a.id === id)!;
        }),
        switchMap((a: Account) =>
          openEditAccountDialog(this.dialog, a, this.accounts, false)
        ),
        takeUntil(this.ngUnsubscribe)
      )
      .subscribe(() => this.router.navigateByUrl('accounts/list'));
  }

  ngOnInit(): void {
    this.loadAccounts();
  }
  @ViewChild('tree') tree: any;
  ngOnDestroy() {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
  addAccountDialog(): void {
    this.router.navigateByUrl('accounts/list/0');
    openEditAccountDialog(
      this.dialog,
      { id: 0, name: '', number: 0, parentId: 0, bookId: 1, children: [] },
      this.accounts,
      true
    );
  }
  loadAccounts(): void {
    this.store.dispatch(loadAccounts());
  }
}
