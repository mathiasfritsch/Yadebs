import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { loadAccounts } from '../store/account.actions';
import {
  selectAccountState,
  selectEntity,
  selectAccountTree,
  selectAllAccounts,
} from '../store/account.selectors';
import {
  AccountEditComponent,
  openEditAccountDialog,
} from '../account-edit/account-edit.component';
import { MatDialog } from '@angular/material/dialog';
import { Router, NavigationEnd, Event } from '@angular/router';
import {
  switchMap,
  filter,
  Subscription,
  withLatestFrom,
  map,
  tap,
} from 'rxjs';
import { FlatTreeControl } from '@angular/cdk/tree';
import {
  MatTreeFlatDataSource,
  MatTreeFlattener,
} from '@angular/material/tree';
import { Account } from 'src/app/shared/account';
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
  styleUrls: ['./account-list.component.css'],
})
export class AccountListComponent implements OnInit {
  private _transformer = (node: AccountNode, level: number) => {
    return {
      expandable: !!node.children && node.children.length > 0,
      name: node.name,
      level: level,
      id: node.id,
      number: node.number,
    };
  };

  treeControl = new FlatTreeControl<AcccountFlatNode>(
    (node) => node.level,
    (node) => node.expandable
  );

  treeFlattener = new MatTreeFlattener(
    this._transformer,
    (node) => node.level,
    (node) => node.expandable,
    (node) => node.children
  );
  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);
  accounts: Account[] = [];
  loading$ = this.store.pipe(select(selectAccountState));
  hasChild = (_: number, node: AcccountFlatNode) => node.expandable;
  selectAccountTreeSubscription: Subscription;
  modalSubscription: Subscription | undefined;

  constructor(
    private store: Store,
    public dialog: MatDialog,
    private router: Router
  ) {
    this.selectAccountTreeSubscription = this.store
      .pipe(select(selectAccountTree))
      .subscribe((accountsAsTree) => {
        this.dataSource.data = accountsAsTree;
        this.treeControl.expandAll();
      });

    this.selectAccountTreeSubscription = this.store
      .pipe(select(selectAllAccounts))
      .subscribe((accounts) => {
        this.accounts = accounts;
      });

    this.dataSource.data = [];

    const navigationEvent$ = router.events.pipe(
      filter(
        (e: Event): e is NavigationEnd =>
          e instanceof NavigationEnd &&
          e.url != '/accounts/list' &&
          e.url != '/accounts/list/0'
      )
    );

    navigationEvent$.subscribe((ne) => {
      if (this.accounts.length > 0) {
        var id = Number(ne.url.split('/')[2]);
        var accounToEdit = this.accounts.find((a) => a.id === id);
        this.modalSubscription = openEditAccountDialog(
          this.dialog,
          accounToEdit!,
          this.accounts
        )
          .pipe(filter((val) => !!val))
          .subscribe((val) => console.log('window cölosed', val));
      }
    });
  }

  ngOnInit(): void {
    this.loadAccounts();
  }
  @ViewChild('tree') tree: any;
  ngOnDestroy() {
    this.selectAccountTreeSubscription.unsubscribe();
    if (this.modalSubscription) this.modalSubscription.unsubscribe();
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
