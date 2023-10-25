import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, switchMap, mergeMap } from 'rxjs/operators';
import { of } from 'rxjs';
import * as JournalActions from './journal.actions';
import { JournalService } from 'src/app/shared/journal.service';
import { JournalUpdate } from 'src/app/shared/JournalUpdate';
import { Update } from '@ngrx/entity';

@Injectable()
export class JournalEffects {
  loadJournals$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(JournalActions.loadJournals),
      switchMap(() =>
        this.journalService.getJournals().pipe(
          map(journals =>
            JournalActions.loadJournalsSuccess({ data: journals })
          ),
          catchError(error => of(JournalActions.loadJournalsFailure({ error })))
        )
      )
    );
  });

  deleteJournal$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(JournalActions.deleteJournal),
      mergeMap(action => {
        return this.journalService.deleteJournal(action.id).pipe(
          map(() => {
            return JournalActions.deleteJournalSuccess({ id: action.id });
          })
        );
      })
    );
  });

  addJournal$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(JournalActions.addJournal),
      mergeMap(action => {
        return this.journalService.addJournal(action.journal).pipe(
          map(data => {
            const journal = { ...action.journal, id: data.id };
            return JournalActions.addJournalSuccess({ journal });
          })
        );
      })
    );
  });

  updateJournal$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(JournalActions.updateJournal),
      mergeMap(action => {
        return this.journalService.updateJournal(action.journal).pipe(
          map(() => {
            const updateJournal: Update<JournalUpdate> = {
              id: action.journal.id,
              changes: {
                ...action.journal,
              },
            };

            return JournalActions.updateJournalSuccess({
              journal: updateJournal,
            });
          })
        );
      })
    );
  });

  constructor(
    private actions$: Actions,
    private journalService: JournalService
  ) {}
}
