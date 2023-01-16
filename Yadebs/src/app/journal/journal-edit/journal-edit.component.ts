import { Component, OnInit, Inject } from '@angular/core';
import {
  MatDialogRef,
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogConfig,
} from '@angular/material/dialog';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Journal } from 'src/app/shared/journal';

import {
  addJournal,
  updateJournal,
  deleteJournal,
} from '../../store/journal/journal.actions';
import { Store, select } from '@ngrx/store';
import { Account } from 'src/app/shared/account';
import { selectAllAccounts } from '../../store/account/account.selectors';
import { loadAccounts } from '../../store/account/account.actions';
import { switchMap, filter, Subject, map, takeUntil } from 'rxjs';

@Component({
  selector: 'app-journal-edit',
  templateUrl: './journal-edit.component.html',
  styleUrls: ['./journal-edit.component.scss'],
})
export class JournalEditComponent {
  accountList: Account[] = [];
  public sourceAccountId: number = 0;
  public targetAccountId: number = 0;
  private ngUnsubscribe = new Subject<void>();
  public state: string = 'NY';

  options = [
    { value: 'NY', label: 'Option NY' },
    { value: 'WA', label: 'Option WA' },
  ];

  constructor(
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<JournalEditComponent>,
    private router: Router,
    private store: Store,
    @Inject(MAT_DIALOG_DATA) public modalData: any
  ) {
    this.store.dispatch(loadAccounts());
    this.store
      .pipe(select(selectAllAccounts), takeUntil(this.ngUnsubscribe))
      .subscribe((accounts) => {
        this.accountList = accounts;
      });
    this.isAdd = modalData.isAdd;
    this.journal = modalData.journal;
    this.state = 'WA';

    this.journalForm = this.formBuilder.group({
      state: [this.state],
      sourceAccountId: [this.journal.transactions[0].account.id],
      targetAccountId: [this.journal.transactions[1].account.id],
      sourceTransactionId: [this.journal.transactions[0].id],
      targetTransactionId: [this.journal.transactions[1].id],
      date: [this.journal.date, Validators.required],
      description: [this.journal.description, Validators.required],
      amount: [this.journal.transactions[0].amount, Validators.required],
    });
  }
  isAdd: boolean = false;

  journal: Journal;
  journalForm: FormGroup;

  closeForm(): void {
    this.dialogRef.close();
    this.router.navigateByUrl('journal/list');
  }

  submitDeleteForm(): void {
    this.store.dispatch(deleteJournal({ id: this.journal.id.toString() }));
    this.dialogRef.close();
  }
  submitAddForm(): void {
    if (this.journalForm.invalid) {
      return;
    }
    const journal: Journal = {
      id: this.journal.id,
      description: this.journalForm.value.description ?? '',
      date: this.journalForm.value.date,
      bookId: 1,
      transactions: [],
    };
    this.store.dispatch(addJournal({ journal }));
    this.dialogRef.close();
  }
  submitUpdateForm(): void {
    if (this.journalForm.invalid) {
      return;
    }

    const journal: Journal = {
      id: this.journal.id,
      description: this.journalForm.value.description ?? '',
      date: this.journalForm.value.date,
      bookId: 1,
      transactions: [
        {
          id: this.journalForm.value.sourceTransactionId,
          journalId: this.journal.id,
          accountId: this.journalForm.value.sourceAccountId,
          amount: this.journalForm.value.amount,
          account: this.accountList[0],
        },
        {
          id: this.journalForm.value.targetTransactionId,
          journalId: this.journal.id,
          accountId: this.journalForm.value.targetAccountId,
          amount: this.journalForm.value.amount,
          account: this.accountList[1],
        },
      ],
    };
    this.store.dispatch(updateJournal({ journal }));
    this.dialogRef.close();
  }
}

export function openEditDialog(
  dialog: MatDialog,
  journal: Journal,
  isAdd: boolean
) {
  const config = new MatDialogConfig();

  let modalData = {
    journal: journal,
    isAdd: isAdd,
  };

  config.disableClose = true;
  config.autoFocus = true;
  config.panelClass = 'modal-panel';
  config.data = {
    ...modalData,
  };
  const dialogRef = dialog.open(JournalEditComponent, config);

  return dialogRef.afterClosed();
}
