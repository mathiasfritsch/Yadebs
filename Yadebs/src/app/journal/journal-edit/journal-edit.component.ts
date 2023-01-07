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
} from '../store/journal.actions';
import { Store, select } from '@ngrx/store';
import { Account } from 'src/app/shared/account';
import { selectAllAccounts } from '../../accounts/store/account.selectors';
import { loadAccounts } from '../../accounts/store/account.actions';
import { switchMap, filter, Subject, map, takeUntil } from 'rxjs';

@Component({
  selector: 'app-journal-edit',
  templateUrl: './journal-edit.component.html',
  styleUrls: ['./journal-edit.component.css'],
})
export class JournalEditComponent {
  accountList: Account[] = [];

  private ngUnsubscribe = new Subject<void>();

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
    this.journalForm = this.formBuilder.group({
      date: [this.journal.date, Validators.required],
      name: [this.journal.name, Validators.required],
      sourceAccountId: [
        this.journal.transactions[0].accountId,
        Validators.required,
      ],
      amount: [this.journal.transactions[0].amount, Validators.required],
      targetAccountId: [
        this.journal.transactions[1].accountId,
        Validators.required,
      ],
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
      name: this.journalForm.value.name ?? '',
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
      name: this.journalForm.value.name ?? '',
      date: this.journalForm.value.date,
      bookId: 1,
      transactions: [],
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
