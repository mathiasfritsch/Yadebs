import { createReducer, on } from '@ngrx/store';
import * as AccountActions from './account.actions';
import { Account } from 'src/app/shared/account';
export const accountFeatureKey = 'account';
import { EntityState, EntityAdapter, createEntityAdapter } from '@ngrx/entity';
export const adapter: EntityAdapter<Account> = createEntityAdapter<Account>({});

export interface AccountState extends EntityState<Account> {
  loading: boolean;
}
export const initialState: AccountState = adapter.getInitialState({
  loading: false,
});

export const reducer = createReducer(
  initialState,
  on(AccountActions.updateAccountSuccess, (state, action) => {
    return adapter.updateOne(action.account, state);
  }),
  on(AccountActions.addAccountSuccess, (state, action) => {
    return adapter.addOne(action.account, state);
  }),
  on(AccountActions.deleteAccountSuccess, (state, action) => {
    return adapter.removeOne(action.id, state);
  }),
  on(AccountActions.loadAccounts, state => {
    return { ...state, loading: true };
  }),
  on(AccountActions.loadAccountsSuccess, (state, action) => {
    return {
      ...adapter.setAll(action.data, state),
      loading: false,
    };
  }),
  on(AccountActions.loadAccountsFailure, state => {
    return { ...state, loading: false };
  })
);

export const { selectAll } = adapter.getSelectors();
