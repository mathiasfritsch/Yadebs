import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { loadJournals, loadJournalsSuccess } from '../store/journal.actions';

import {
  selectJournalState,
  selectAllJournals,
} from '../store/journal.selectors';

import { switchMap, filter, Subject, map, takeUntil } from 'rxjs';
import { Journal } from 'src/app/shared/journal';

@Component({
  selector: 'app-journal-list',
  templateUrl: './journal-list.component.html',
  styleUrls: ['./journal-list.component.css'],
})
export class JournalListComponent implements OnInit {
  displayedColumns: string[] = ['id', 'edit', 'date', 'debit', 'credit'];

  private ngUnsubscribe = new Subject<void>();
  journals: Journal[] = [];

  constructor(private store: Store) {
    this.store
      .pipe(select(selectAllJournals), takeUntil(this.ngUnsubscribe))
      .subscribe((journals) => {
        this.journals = journals;
      });
  }
  editJournal(id: number) {
    console.log('edit' + id);
  }
  ngOnInit(): void {
    this.loadJournals();
  }
  loadJournals(): void {
    this.store.dispatch(loadJournals());
  }
  ngOnDestroy() {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
