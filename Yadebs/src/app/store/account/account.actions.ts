import { createAction, props } from '@ngrx/store';
import { Account } from 'src/app/shared/account';
import { HttpErrorResponse } from '@angular/common/http';
export const ADD_ACCOUNT_ACTION = '[Account] add account';
export const ADD_ACCOUNT_SUCCESS = '[Account] add account success';
export const UPDATE_ACCOUNT_ACTION = '[Account] update account';
export const UPDATE_ACCOUNT_SUCCESS = '[Account] update account success';
export const DELETE_ACCOUNT_ACTION = '[Account] delete account';
export const DELETE_ACCOUNT_SUCCESS = '[Account] delete account success';
import { Update } from '@ngrx/entity';
import { AccountUpdate } from 'src/app/shared/AccountUpdate';
export const loadAccounts = createAction('[Account] Load Accounts');

export const loadAccountsSuccess = createAction(
  '[Account] Load Accounts Success',
  props<{ data: Account[] }>()
);

export const loadAccountsFailure = createAction(
  '[Account] Load Accounts Failure',
  props<{ error: HttpErrorResponse }>()
);

export const deleteAccount = createAction(
  DELETE_ACCOUNT_ACTION,
  props<{ id: string }>()
);

export const deleteAccountSuccess = createAction(
  DELETE_ACCOUNT_SUCCESS,
  props<{ id: string }>()
);

export const addAccount = createAction(
  ADD_ACCOUNT_ACTION,
  props<{ account: Account }>()
);

export const addAccountSuccess = createAction(
  ADD_ACCOUNT_SUCCESS,
  props<{ account: Account }>()
);

export const updateAccount = createAction(
  UPDATE_ACCOUNT_ACTION,
  props<{ account: Account }>()
);

export const updateAccountSuccess = createAction(
  UPDATE_ACCOUNT_SUCCESS,
  props<{ account: Update<AccountUpdate> }>()
);

export const deleteAccountFailure = createAction(
  '[Account] Delete Account Failure',
  props<{ error: HttpErrorResponse }>()
);

export const addAccountFailure = createAction(
  '[Account] Add Account Failure',
  props<{ error: HttpErrorResponse }>()
);

export const updateAccountFailure = createAction(
  '[Account] Update Account Failure',
  props<{ error: HttpErrorResponse }>()
);
