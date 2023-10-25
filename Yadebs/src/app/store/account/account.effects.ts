import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, switchMap, tap, mergeMap } from 'rxjs/operators';
import { of } from 'rxjs';
import * as AccountActions from './account.actions';
import { AccountService } from 'src/app/shared/account.service';
import { Update } from '@ngrx/entity';
import { AccountUpdate } from 'src/app/shared/AccountUpdate';

@Injectable()
export class AccountEffects {
  loadAccounts$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(AccountActions.loadAccounts),
      tap(x => {
        console.log('loadAccounts called');
      }),
      switchMap(() =>
        this.accountService.getAccounts().pipe(
          map(accounts =>
            AccountActions.loadAccountsSuccess({ data: accounts })
          ),
          catchError(error => of(AccountActions.loadAccountsFailure({ error })))
        )
      )
    );
  });

  deleteAccount$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(AccountActions.deleteAccount),
      mergeMap(action => {
        return this.accountService.deleteAccount(action.id).pipe(
          map(data => {
            return AccountActions.deleteAccountSuccess({ id: action.id });
          })
        );
      })
    );
  });

  addAccount$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(AccountActions.addAccount),
      mergeMap(action => {
        return this.accountService.addAccount(action.account).pipe(
          map(data => {
            const account = { ...action.account, id: data.id };
            return AccountActions.addAccountSuccess({ account });
          })
        );
      })
    );
  });

  updateAccount$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(AccountActions.updateAccount),
      mergeMap(action => {
        return this.accountService.updateAccount(action.account).pipe(
          map(data => {
            const updateAccount: Update<AccountUpdate> = {
              id: action.account.id,
              changes: {
                ...action.account,
              },
            };

            return AccountActions.updateAccountSuccess({
              account: updateAccount,
            });
          })
        );
      })
    );
  });

  constructor(
    private actions$: Actions,
    private accountService: AccountService
  ) {}
}
