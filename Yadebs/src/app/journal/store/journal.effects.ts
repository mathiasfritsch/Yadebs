import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, switchMap, tap } from 'rxjs/operators';
import { of } from 'rxjs';
import * as JournalActions from './journal.actions';
import { JournalService } from 'src/app/shared/journal.service';

@Injectable()
export class JournalEffects {
  loadJournals$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(JournalActions.loadJournals),
      tap((x) => {
        console.log('loadJournals called');
      }),
      switchMap(() =>
        this.ps.getJournals().pipe(
          map((books) => JournalActions.loadJournalsSuccess({ data: books })),
          catchError((error) =>
            of(JournalActions.loadJournalsFailure({ error }))
          )
        )
      )
    );
  });

  constructor(private actions$: Actions, private ps: JournalService) {}
}
