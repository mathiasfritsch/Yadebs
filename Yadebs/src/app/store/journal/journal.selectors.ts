import { getMatFormFieldMissingControlError } from '@angular/material/form-field';
import { createFeatureSelector, createSelector } from '@ngrx/store';
import { Account } from 'src/app/shared/account';
import { Journal } from 'src/app/shared/journal';
import { selectAllAccounts } from '../account/account.selectors';
import * as fromJournal from './journal.reducer';

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

export const selectJournalsWithAccounts = createSelector(
  selectAllJournals,
  selectAllAccounts,
  (journals, accounts) => ({ journals, accounts })
);

export const selectJournalListViewModel = createSelector(
  selectAllJournals,
  selectAllAccounts,
  (journals, accounts) =>
    journals.map((j) => mapJournalListViewModel(j, accounts))
);

export interface JournalListViewModel {
  id: number;
  date: Date;
  name: string;
  bookId: number;
  debitAmount: number;
  debitAccountName: string;
  creditAmount: number;
  creditAccountName: string;
}

function mapJournalListViewModel(
  journal: Journal,
  accounts: Account[]
): JournalListViewModel {
  return {
    id: journal.id,
    date: journal.date,
    name: journal.name,
    bookId: journal.bookId,
    debitAmount: journal.transactions[0].amount,
    debitAccountName: accounts?.find(
      (a) => a.id === journal.transactions[0].account.id
    )?.name!,
    creditAmount: journal.transactions[1].amount,
    creditAccountName: accounts?.find(
      (a) => a.id === journal.transactions[1].account.id
    )?.name!,
  };
}
