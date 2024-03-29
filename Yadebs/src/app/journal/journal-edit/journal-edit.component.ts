import { Component, Inject } from '@angular/core';
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
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-journal-edit',
  templateUrl: './journal-edit.component.html',
  styleUrls: ['./journal-edit.component.scss'],
})
export class JournalEditComponent {
  accountList: Account[] = [];
  public sourceAccountId = 0;
  public targetAccountId = 0;
  private ngUnsubscribe = new Subject<void>();


  constructor(
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<JournalEditComponent>,
    private router: Router,
    private store: Store,
    @Inject(MAT_DIALOG_DATA) public modalData: JournalEditComponent
  ) {
    this.store.dispatch(loadAccounts());
    this.store
      .pipe(select(selectAllAccounts), takeUntil(this.ngUnsubscribe))
      .subscribe(accounts => {
        this.accountList = accounts;
      });
    this.isAdd = modalData.isAdd;
    this.journal = modalData.journal;

    if (!this.isAdd) {
      this.journalForm = this.formBuilder.group({
        sourceAccountId: [this.journal.transactions[0].accountId],
        targetAccountId: [this.journal.transactions[1].accountId],
        sourceTransactionId: [this.journal.transactions[0].id],
        targetTransactionId: [this.journal.transactions[1].id],
        date: [this.journal.date, Validators.required],
        description: [this.journal.description, Validators.required],
        amount: [this.journal.transactions[0].amount, Validators.required],
      });
    } else {
      this.journalForm = this.formBuilder.group({
        sourceAccountId: this.accountList[0].id,
        targetAccountId: this.accountList[0].id,
        sourceTransactionId: null,
        targetTransactionId: null,
        date: null,
        description: '',
        amount: 0,
      });
    }
  }
  isAdd = false;

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
      transactions: [
        {
          id: 0,
          journalId: this.journal.id,
          accountId: this.journalForm.value.sourceAccountId,
          amount: this.journalForm.value.amount,
          account: this.accountList[0],
        },
        {
          id: 0,
          journalId: this.journal.id,
          accountId: this.journalForm.value.targetAccountId,
          amount: this.journalForm.value.amount,
          account: this.accountList[1],
        },
      ],
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
          id: this.journal.transactions[0].id,
          journalId: this.journal.id,
          accountId: this.journalForm.value.sourceAccountId,
          amount: this.journalForm.value.amount,
          account: this.accountList[0],
        },
        {
          id: this.journal.transactions[1].id,
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

  const modalData = {
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
