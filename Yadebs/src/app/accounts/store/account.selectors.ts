import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as fromAccount from './account.reducer';

export const selectAccountState = createFeatureSelector<fromAccount.State>(
  fromAccount.accountFeatureKey
);

export const selectSelectorsLoading = createSelector(
  selectAccountState,
  (state) => state.loading
);

export const selectAllAccounts = createSelector(
  selectAccountState,
  fromAccount.selectAll
);
export const selectAccount = createSelector(
  selectAccountState,
  (state) => state.entities[1]
);
