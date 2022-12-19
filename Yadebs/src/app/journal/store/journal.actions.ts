import { createAction, props } from '@ngrx/store';
import { Journal } from 'src/app/shared/journal';
import { HttpErrorResponse } from '@angular/common/http';

export const loadJournals = createAction('[Journal] Load Journals');

export const loadJournalsSuccess = createAction(
  '[Journal] Load Journals Success',
  props<{ data: Journal[] }>()
);

export const loadJournalsFailure = createAction(
  '[Journal] Load Journals Failure',
  props<{ error: HttpErrorResponse }>()
);
