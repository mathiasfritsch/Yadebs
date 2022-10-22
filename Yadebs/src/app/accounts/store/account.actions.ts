import { createAction, props } from '@ngrx/store';
import { Account } from 'src/app/shared/account';
import { HttpErrorResponse } from '@angular/common/http';

export const loadAccounts = createAction('[Account] Load Accounts');

export const setEditAccount = createAction(
  '[Account] Set Edit Account',
  props<{ data: Account }>()
);

export const loadAccountsSuccess = createAction(
  '[Account] Load Accounts Success',
  props<{ data: Account[] }>()
);

export const loadAccountsFailure = createAction(
  '[Account] Load Accounts Failure',
  props<{ error: HttpErrorResponse }>()
);
