import { Transaction } from './transaction';
export interface Journal {
  id: number;
  name: string;
  date: Date;
  bookId: number;
  transactions: Transaction[];
}
