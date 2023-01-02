import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { loadJournals, loadJournalsSuccess } from '../store/journal.actions';

import {
  selectJournalState,
  selectAllJournals,
  selectAllJournalsWithAccounts,
} from '../store/journal.selectors';

import { switchMap, filter, Subject, map, takeUntil } from 'rxjs';
import { Journal } from 'src/app/shared/journal';
import { Router, NavigationEnd, Event } from '@angular/router';
import { Account } from 'src/app/shared/account';
import { openEditDialog } from '../journal-edit/journal-edit.component';
import { MatDialog } from '@angular/material/dialog';
import { loadAccounts } from 'src/app/accounts/store/account.actions';
@Component({
  selector: 'app-journal-list',
  templateUrl: './journal-list.component.html',
  styleUrls: ['./journal-list.component.css'],
})
export class JournalListComponent implements OnInit {
  displayedColumns: string[] = ['id', 'edit', 'date', 'debit', 'credit'];

  private ngUnsubscribe = new Subject<void>();
  journals: Journal[] = [];

  constructor(
    private store: Store,
    private router: Router,
    public dialog: MatDialog
  ) {
    this.store
      .pipe(select(selectAllJournals), takeUntil(this.ngUnsubscribe))
      .subscribe((journals) => {
        this.journals = journals;
      });

    router.events
      .pipe(
        filter(
          (e: Event): e is NavigationEnd =>
            e instanceof NavigationEnd &&
            e.url != '/journal/list' &&
            e.url != '/journal/list/0'
        ),
        filter(() => this.journals.length > 0),
        map((ne) => Number(ne.url.split('/')[3])),
        map((id) => this.journals.find((a) => a.id === id)!),
        switchMap((j: Journal) => openEditDialog(this.dialog, j, false)),
        takeUntil(this.ngUnsubscribe)
      )
      .subscribe(() => this.router.navigateByUrl('journal/list'));
  }
  addJournal() {
    this.router.navigateByUrl(`journal/list/0`);

    openEditDialog(
      this.dialog,
      { id: 0, name: '', date: new Date(), bookId: 1, transactions: [] },
      true
    );
  }
  editJournal(id: number) {
    this.router.navigateByUrl(`journal/list/${id}`);
  }
  ngOnInit(): void {
    this.store.dispatch(loadJournals());
  }

  ngOnDestroy() {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
