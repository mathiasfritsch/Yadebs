import { createFeatureSelector, createSelector } from '@ngrx/store';
import { Journal } from 'src/app/shared/journal';
import * as journalReducer from './journal.reducer';

export const selectJournalState = createFeatureSelector<journalReducer.State>(
  journalReducer.journalFeatureKey
);

export const selectSelectorsLoading = createSelector(
  selectJournalState,
  (state) => state.loading
);

export const selectAllJournals = createSelector(
  selectJournalState,
  journalReducer.selectAll
);

export const selectEntity = (id: number) =>
  createSelector(selectJournalState, (state) => state.entities[id]);
