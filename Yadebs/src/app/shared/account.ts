export interface Account {
  id: number;
  name: string;
  number: number;
  parentId: number;
  bookId: number;
  children: Account[];
}
