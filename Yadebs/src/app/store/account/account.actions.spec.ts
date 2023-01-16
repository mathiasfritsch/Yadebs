import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { getTestScheduler, cold } from 'jasmine-marbles';
import { map, tap } from 'rxjs/operators';
import { combineLatest, merge } from 'rxjs';
import { initialState, reducer } from './account.reducer';
import {
  deleteAccount,
  loadAccounts,
  loadAccountsSuccess,
} from './account.actions';
import { Account } from 'src/app/shared/account';

describe('AccountState Reducer', () => {
  it('should return the initial state', () => {
    const action = { type: 'NOOP' } as any;
    const result = reducer(initialState, action);

    expect(result).toBe(initialState);
  });
  it('should set loading flag', () => {
    const action = loadAccounts;
    const result = reducer(initialState, action);

    expect(result.loading).toBe(true);
  });

  it('should set loading flag to false', () => {
    const accounts: Account[] = [
      {
        id: 11,
        name: 'somename',
        bookId: 1122,
        parentId: 121,
        children: [],
        number: 232,
      },
    ];

    const action = loadAccountsSuccess({ data: accounts });
    const result = reducer(initialState, action);

    expect(result.loading).toBe(false);
    expect(result.entities).toBeDefined();
  });
});
