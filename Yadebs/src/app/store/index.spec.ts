import { TestBed } from '@angular/core/testing';
import { StoreModule } from '@ngrx/store';
import { provideMockStore, MockStore } from '@ngrx/store/testing';
import { reducers } from '.';
import {
  adapter,
  JournalState,
  reducer,
  selectAll,
} from './journal/journal.reducer';
import { selectAllJournals, selectJournal } from './journal/journal.selectors';
describe('SelectorTest', () => {
  let store: MockStore;

  const initialState: JournalState = adapter.getInitialState({
    loading: false,
  });

  it('should select all journals', () => {
    const result = selectAllJournals.projector(initialState);
    console.log(result.length);
  });
});
