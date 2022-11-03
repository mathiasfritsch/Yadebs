import { createAction, props } from '@ngrx/store';
import { Account } from 'src/app/shared/account';
import { HttpErrorResponse } from '@angular/common/http';
export const ADD_ACCOUNT_ACTION = '[Account] add account';
export const ADD_ACCOUNT_SUCCESS = '[Account] add account success';

export const loadAccounts = createAction('[Account] Load Accounts');

export const loadAccountsSuccess = createAction(
  '[Account] Load Accounts Success',
  props<{ data: Account[] }>()
);

export const loadAccountsFailure = createAction(
  '[Account] Load Accounts Failure',
  props<{ error: HttpErrorResponse }>()
);

export const addAccount = createAction(
  ADD_ACCOUNT_ACTION,
  props<{ account: Account }>()
);

export const addAccountSuccess = createAction(
  ADD_ACCOUNT_SUCCESS,
  props<{ account: Account }>()
);

export const addAccountFailure = createAction(
  '[Account] Add Account Failure',
  props<{ error: HttpErrorResponse }>()
);
