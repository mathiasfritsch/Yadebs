import { isDevMode } from '@angular/core';
import {
  ActionReducer,
  ActionReducerMap,
  createFeatureSelector,
  createSelector,
  MetaReducer,
} from '@ngrx/store';
import {
  accountFeatureKey,
  AccountState,
  reducer as AccountReducer,
} from '../accounts/store/account.reducer';

import {
  journalFeatureKey,
  JournalState,
  reducer as JournalReducer,
} from '../journal/store/journal.reducer';

export interface AppState {
  [accountFeatureKey]: AccountState;
  [journalFeatureKey]: JournalState;
}

export const reducers: ActionReducerMap<AppState> = {
  [accountFeatureKey]: AccountReducer,
  [journalFeatureKey]: JournalReducer,
};

export const metaReducers: MetaReducer<AppState>[] = isDevMode() ? [] : [];
