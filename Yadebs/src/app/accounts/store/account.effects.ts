import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, switchMap, tap } from 'rxjs/operators';
import { of } from 'rxjs';
import * as AccountActions from './account.actions';
import { AccountService } from 'src/app/shared/account.service';

@Injectable()
export class AccountEffects {
  loadAccounts$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(AccountActions.loadAccounts),
      tap((x) => {
        console.log('loadAccounts called');
      }),
      switchMap(() =>
        this.accountService.getAccounts().pipe(
          map((accounts) =>
            AccountActions.loadAccountsSuccess({ data: accounts })
          ),
          catchError((error) =>
            of(AccountActions.loadAccountsFailure({ error }))
          )
        )
      )
    );
  });

  constructor(
    private actions$: Actions,
    private accountService: AccountService
  ) {}
}
