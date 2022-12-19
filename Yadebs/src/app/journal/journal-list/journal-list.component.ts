import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { loadJournals, loadJournalsSuccess } from '../store/journal.actions';

import {
  selectJournalState,
  selectAllJournals,
} from '../store/journal.selectors';

import { switchMap, filter, Subject, map, takeUntil } from 'rxjs';

@Component({
  selector: 'app-journal-list',
  templateUrl: './journal-list.component.html',
  styleUrls: ['./journal-list.component.css'],
})
export class JournalListComponent implements OnInit {
  private ngUnsubscribe = new Subject<void>();
  journals$ = this.store.pipe(select(selectAllJournals));
  constructor(private store: Store) {}

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
