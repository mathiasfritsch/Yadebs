import { Component, OnInit, Inject, ViewChild } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { loadAccounts } from '../store/account.actions';
import {
  selectAccountState,
  selectAllAccounts,
  selectEntity,
  selectAccountTree,
} from '../store/account.selectors';
import { ActivatedRoute } from '@angular/router';
import {
  AccountEditComponent,
  openEditCourseDialog,
} from '../account-edit/account-edit.component';
import { MatDialog } from '@angular/material/dialog';
import { Router, NavigationEnd, Event } from '@angular/router';
import { switchMap, filter } from 'rxjs';
import { MatIcon } from '@angular/material/icon';
import { FlatTreeControl } from '@angular/cdk/tree';
import {
  MatTreeFlatDataSource,
  MatTreeFlattener,
} from '@angular/material/tree';
interface AccountNode {
  name: string;
  id: number;
  children?: AccountNode[];
}

interface AcccountFlatNode {
  expandable: boolean;
  name: string;
  level: number;
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

  accounts$ = this.store.pipe(select(selectAllAccounts));

  loading$ = this.store.pipe(select(selectAccountState));
  hasChild = (_: number, node: AcccountFlatNode) => node.expandable;

  constructor(
    private store: Store,
    private route: ActivatedRoute,
    public dialog: MatDialog,
    private router: Router
  ) {
    this.store.pipe(select(selectAccountTree)).subscribe((accounts) => {
      this.dataSource.data = accounts;
      this.treeControl.expandAll();
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
        switchMap((re) =>
          this.store.pipe(
            select(
              selectEntity(
                ((): number => {
                  var urlParts = re.url.split('/');
                  return Number(urlParts[urlParts.length - 1]);
                })()
              )
            )
          )
        )
      )
      .subscribe((accountSelected) => {
        if (accountSelected)
          openEditCourseDialog(this.dialog, accountSelected)
            .pipe(filter((val) => !!val))
            .subscribe((val) => console.log('new course value:', val));
      });
  }

  ngOnInit(): void {
    this.loadAccounts();
  }
  @ViewChild('tree') tree: any;

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
