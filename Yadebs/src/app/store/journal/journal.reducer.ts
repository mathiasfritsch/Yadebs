import { Action, createReducer, on, createFeatureSelector } from '@ngrx/store';
import * as JournalActions from './journal.actions';
import { Journal } from 'src/app/shared/journal';
import { state } from '@angular/animations';
export const journalFeatureKey = 'journal';
import { EntityState, EntityAdapter, createEntityAdapter } from '@ngrx/entity';
import { createSelector } from '@ngrx/store';

export const adapter: EntityAdapter<Journal> = createEntityAdapter<Journal>({});

export interface JournalState extends EntityState<Journal> {
  loading: boolean;
}
export const initialState: JournalState = adapter.getInitialState({
  loading: false,
});

export const reducer = createReducer(
  initialState,
  on(JournalActions.updateJournalSuccess, (state, action) => {
    return adapter.updateOne(action.journal, state);
  }),
  on(JournalActions.addJournalSuccess, (state, action) => {
    return adapter.addOne(action.journal, state);
  }),
  on(JournalActions.deleteJournalSuccess, (state, action) => {
    return adapter.removeOne(action.id, state);
  }),
  on(JournalActions.loadJournals, state => {
    return { ...state, loading: true };
  }),
  on(JournalActions.loadJournalsSuccess, (state, action) => {
    return {
      ...adapter.setAll(action.data, state),
      loading: false,
    };
  }),
  on(JournalActions.loadJournalsFailure, (state, action) => {
    return { ...state, loading: false };
  })
);

export const { selectAll } = adapter.getSelectors();
