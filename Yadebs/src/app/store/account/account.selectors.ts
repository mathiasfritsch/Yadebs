import { createFeatureSelector, createSelector } from '@ngrx/store';
import { Account } from 'src/app/shared/account';
import * as fromAccount from './account.reducer';

export const selectAccountState =
  createFeatureSelector<fromAccount.AccountState>(
    fromAccount.accountFeatureKey
  );

export const selectSelectorsLoading = createSelector(
  selectAccountState,
  state => state.loading
);

export const selectAllAccounts = createSelector(
  selectAccountState,
  fromAccount.selectAll
);

export const selectEntity = (id: number) =>
  createSelector(selectAccountState, state => state.entities[id]);

export const selectAccountTree = createSelector(selectAllAccounts, accounts =>
  getTree(accounts)
);

function getTree(nodes: Account[]): Account[] {
  const mutableNodes: Account[] = JSON.parse(JSON.stringify(nodes));
  const tree = new Array<Account>();
  mutableNodes
    .filter(n => n.parentId === null)
    .forEach(n => tree.push(getNodeWithChildren(mutableNodes, n)));
  return tree;
}

function getNodeWithChildren(nodes: Account[], node: Account): Account {
  const children = new Array<Account>();
  nodes
    .filter(n => n.parentId === node.id)
    .forEach(n => children.push(getNodeWithChildren(nodes, n)));

  if (children.length > 0) {
    node.children = children;
  }

  return node;
}
