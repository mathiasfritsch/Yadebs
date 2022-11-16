import { createFeatureSelector, createSelector } from '@ngrx/store';
import { Account } from 'src/app/shared/account';
import { AccountEditComponent } from '../account-edit/account-edit.component';
import * as fromAccount from './account.reducer';

export const selectAccountState = createFeatureSelector<fromAccount.State>(
  fromAccount.accountFeatureKey
);

export const selectSelectorsLoading = createSelector(
  selectAccountState,
  (state) => state.loading
);

export const selectAllAccounts = createSelector(
  selectAccountState,
  fromAccount.selectAll
);

export const selectEntity = (id: number) =>
  createSelector(selectAccountState, (state) => state.entities[id]);

export const selectAccountTree = createSelector(selectAllAccounts, (accounts) =>
  getTree(accounts)
);

function getTree(nodes: Account[]): Account[] {
  var mutableNodes: Account[] = JSON.parse(JSON.stringify(nodes));
  var tree = new Array<Account>();
  mutableNodes
    .filter((n) => n.parentId === null)
    .forEach((n) => tree.push(getNodeWithChildren(mutableNodes, n)));
  return tree;
}

function getNodeWithChildren(nodes: Account[], node: Account): Account {
  var children = new Array<Account>();
  nodes
    .filter((n) => n.parentId === node.id)
    .forEach((n) => children.push(getNodeWithChildren(nodes, n)));

  if (children.length > 0) {
    node.children = children;
  }

  return node;
}
