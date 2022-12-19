import { Account } from './account';

export interface Transaction {
  id: number;
  journalId: number;
  accountId: number;
  account: Account;
  amount: number;
}
