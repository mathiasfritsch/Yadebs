import { createReducer, on } from '@ngrx/store';
import { Journal } from 'src/app/shared/journal';
export const journalFeatureKey = 'journal';
import { EntityState, EntityAdapter, createEntityAdapter } from '@ngrx/entity';
export const adapter: EntityAdapter<Journal> = createEntityAdapter<Journal>({});
import * as JournalActions from './journal.actions';
export interface State extends EntityState<Journal> {
  loading: boolean;
  editJournal: any;
}
export const initialState: State = adapter.getInitialState({
  loading: false,
  editJournal: null,
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
  on(JournalActions.loadJournals, (state) => {
    return { ...state, loading: true };
  }),
  on(JournalActions.loadJournalsSuccess, (state, action) => {
    return {
      ...adapter.setAll(action.data, state),
      journals: action.data,
      loading: false,
    };
  }),
  on(JournalActions.loadJournalsFailure, (state, action) => {
    return { ...state, loading: false };
  })
);
export const { selectAll } = adapter.getSelectors();
