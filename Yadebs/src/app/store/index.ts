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
export interface AppState {
  [accountFeatureKey]: AccountState;
}

export const reducers: ActionReducerMap<AppState> = {
  [accountFeatureKey]: AccountReducer,
};

export const metaReducers: MetaReducer<AppState>[] = isDevMode() ? [] : [];
