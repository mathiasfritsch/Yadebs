import { Action, createReducer, on, createFeatureSelector } from '@ngrx/store';
import * as AccountActions from './account.actions';
import { Account } from 'src/app/shared/account';
import { state } from '@angular/animations';
export const accountFeatureKey = 'account';
import { EntityState, EntityAdapter, createEntityAdapter } from '@ngrx/entity';
import { createSelector } from '@ngrx/store';

export const adapter: EntityAdapter<Account> = createEntityAdapter<Account>({});

export interface State extends EntityState<Account> {
  loading: boolean;
}
const accountFeature = createFeatureSelector(accountFeatureKey);

export const initialState: State = adapter.getInitialState({
  loading: false,
});

export const reducer = createReducer(
  initialState,

  on(AccountActions.loadAccounts, (state) => {
    return { ...state, loading: true };
  }),
  on(AccountActions.loadAccountsSuccess, (state, action) => {
    return {
      ...adapter.setAll(action.data, state),
      accounts: action.data,
      loading: false,
    };
  }),
  on(AccountActions.loadAccountsFailure, (state, action) => {
    return { ...state, loading: false };
  })
);

export const { selectAll } = adapter.getSelectors();
