import { Transaction } from './transaction';
export interface Journal {
  id: number;
  description: string;
  date: Date;
  bookId: number;
  transactions: Transaction[];
}
