import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as fromJournal from './journal.reducer';

export const selectJournalState = createFeatureSelector<fromJournal.State>(
  fromJournal.journalFeatureKey
);

export const selectSelectorsLoading = createSelector(
  selectJournalState,
  (state) => state.loading
);

export const selectAllJournals = createSelector(
  selectJournalState,
  fromJournal.selectAll
);
export const selectJournal = createSelector(
  selectJournalState,
  (state) => state.entities[1]
);
