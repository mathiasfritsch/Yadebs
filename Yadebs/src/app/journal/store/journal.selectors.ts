import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as fromJournal from './journal.reducer';

import {
  selectAccountState,
  selectAccountTree,
  selectAllAccounts,
} from './../../accounts/store/account.selectors';

export const selectAllJournalsWithAccounts = createSelector(
  fromJournal.selectAll,
  selectAllAccounts,
  (journalEntries, accountEntries) => {
    return { journals: journalEntries, accounts: accountEntries };
  }
);

export const selectJournalState =
  createFeatureSelector<fromJournal.JournalState>(
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
