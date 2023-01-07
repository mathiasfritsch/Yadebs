import { createAction, props } from '@ngrx/store';
import { Journal } from 'src/app/shared/journal';
import { HttpErrorResponse } from '@angular/common/http';
export const ADD_JOURNAL_ACTION = '[Journal] add journal';
export const ADD_JOURNAL_SUCCESS = '[Journal] add journal success';
export const UPDATE_JOURNAL_ACTION = '[Journal] update journal';
export const UPDATE_JOURNAL_SUCCESS = '[Journal] update journal success';
export const DELETE_JOURNAL_ACTION = '[Journal] delete journal';
export const DELETE_JOURNAL_SUCCESS = '[Journal] delete journal success';
import { Update } from '@ngrx/entity';
import { JournalUpdate } from 'src/app/shared/JournalUpdate';
export const loadJournals = createAction('[Journal] Load Journals');

export const loadJournalsSuccess = createAction(
  '[Journal] Load Journals Success',
  props<{ data: Journal[] }>()
);

export const loadJournalsFailure = createAction(
  '[Journal] Load Journals Failure',
  props<{ error: HttpErrorResponse }>()
);

export const deleteJournal = createAction(
  DELETE_JOURNAL_ACTION,
  props<{ id: string }>()
);

export const deleteJournalSuccess = createAction(
  DELETE_JOURNAL_SUCCESS,
  props<{ id: string }>()
);

export const addJournal = createAction(
  ADD_JOURNAL_ACTION,
  props<{ journal: Journal }>()
);

export const addJournalSuccess = createAction(
  ADD_JOURNAL_SUCCESS,
  props<{ journal: Journal }>()
);

export const updateJournal = createAction(
  UPDATE_JOURNAL_ACTION,
  props<{ journal: Journal }>()
);

export const updateJournalSuccess = createAction(
  UPDATE_JOURNAL_SUCCESS,
  props<{ journal: Update<JournalUpdate> }>()
);

export const deleteJournalFailure = createAction(
  '[Journal] Delete Journal Failure',
  props<{ error: HttpErrorResponse }>()
);

export const addJournalFailure = createAction(
  '[Journal] Add Journal Failure',
  props<{ error: HttpErrorResponse }>()
);

export const updateJournalFailure = createAction(
  '[Journal] Update Journal Failure',
  props<{ error: HttpErrorResponse }>()
);
