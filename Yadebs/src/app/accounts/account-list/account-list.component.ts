import { Component, OnInit, Inject, ViewChild } from '@angular/core';
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
import { MatDialog } from '@angular/material/dialog';
import { Router, NavigationEnd, Event } from '@angular/router';
import { switchMap, filter } from 'rxjs';
import { MatIcon } from '@angular/material/icon';
import { FlatTreeControl } from '@angular/cdk/tree';
import {
  MatTreeFlatDataSource,
  MatTreeFlattener,
} from '@angular/material/tree';
interface Family {
  name: string;
  children?: Family[];
}

const FAMILY_TREE: Family[] = [
  {
    name: 'Joyce',
    children: [
      { name: 'Mike' },
      { name: 'Will' },
      { name: 'Eleven', children: [{ name: 'Hopper' }] },
      { name: 'Lucas' },
      { name: 'Dustin', children: [{ name: 'Winona' }] },
    ],
  },
  {
    name: 'Jean',
    children: [{ name: 'Otis' }, { name: 'Maeve' }],
  },
];
interface ExampleFlatNode {
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
  private _transformer = (node: Family, level: number) => {
    return {
      expandable: !!node.children && node.children.length > 0,
      name: node.name,
      level: level,
    };
  };

  treeControl = new FlatTreeControl<ExampleFlatNode>(
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
  hasChild = (_: number, node: ExampleFlatNode) => node.expandable;

  constructor(
    private store: Store,
    private route: ActivatedRoute,
    public dialog: MatDialog,
    private router: Router
  ) {
    this.dataSource.data = FAMILY_TREE;

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

  ngAfterViewInit() {
    this.tree.treeControl.expandAll();
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
