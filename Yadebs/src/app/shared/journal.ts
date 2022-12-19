import { Transaction } from './transaction';
export interface Journal {
  id: number;
  date: Date;
  bookId: number;
  transactions: Transaction[];
}
