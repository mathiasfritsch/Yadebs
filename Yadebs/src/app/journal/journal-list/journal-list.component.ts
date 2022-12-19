import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import {
  loadJournals,
  loadJournalsSuccess,
  updateJournal,
} from '../store/journal.actions';
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
  //loading$ = this.store.pipe(select(selectJournalState));
  //journals$ = this.store.pipe(select(selectAllJournals));
  journals: any;
  constructor(private store: Store) {
    // this.store
    //   .pipe(select(selectAllJournals), takeUntil(this.ngUnsubscribe))
    //   .subscribe((allJournals) => {
    //     this.journals = allJournals;
    //   });
  }

  ngOnInit(): void {
    this.loadJournals();
  }
  loadJournals(): void {
    this.store.dispatch(
      updateJournal({
        journal: {
          id: 1,
          date: new Date('2022-10-10'),
          bookId: 1,
          transactions: [],
        },
      })
    );
    //  this.store.dispatch(loadJournals());
  }
  ngOnDestroy() {
    this.ngUnsubscribe.next();
    this.ngUnsubscribe.complete();
  }
}
