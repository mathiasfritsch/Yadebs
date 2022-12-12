import { Component, OnInit, Inject } from '@angular/core';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import {
  MatDialogRef,
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogConfig,
} from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Account } from 'src/app/shared/account';
import {
  addAccount,
  updateAccount,
  deleteAccount,
} from '../store/account.actions';
import { Store } from '@ngrx/store';

@Component({
  selector: 'app-account-edit',
  templateUrl: './account-edit.component.html',
  styleUrls: ['./account-edit.component.css'],
})
export class AccountEditComponent implements OnInit {
  isAdd: boolean = false;
  account: Account;
  accountForm: FormGroup;

  accountList: Account[];
  selectedValue: number;

  constructor(
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<AccountEditComponent>,
    private router: Router,
    private store: Store,
    @Inject(MAT_DIALOG_DATA) public modalData: any
  ) {
    this.isAdd = modalData.isAdd;
    this.account = modalData.account;

    this.accountForm = this.formBuilder.group({
      name: [this.account.name, Validators.required],
      number: [this.account.number, Validators.required],
    });

    this.accountList = modalData.accountList.filter(
      (a: Account) => a.id != this.account.id
    );
    this.selectedValue = this.account.parentId;
  }

  ngOnInit(): void {}

  closeForm(): void {
    this.dialogRef.close();
    this.router.navigateByUrl('accounts/list');
  }

  submitDeleteForm(): void {
    this.store.dispatch(deleteAccount({ id: this.account.id.toString() }));
    this.dialogRef.close();
  }

  submitAddForm(): void {
    if (this.accountForm.invalid) {
      return;
    }

    const account: Account = {
      id: 0,
      name: this.accountForm.value.name ?? '',
      number: this.accountForm.value.number ?? 0,
      bookId: this.account.bookId,
      parentId: this.selectedValue,
      children: [],
    };

    this.store.dispatch(addAccount({ account }));
    this.dialogRef.close();
  }
  submitUpdateForm(): void {
    if (this.accountForm.invalid) {
      return;
    }
    const account: Account = {
      id: this.account.id,
      name: this.accountForm.value.name ?? '',
      number: this.accountForm.value.number ?? 0,
      bookId: this.account.bookId,
      parentId: this.selectedValue,
      children: [],
    };

    this.store.dispatch(updateAccount({ account }));
    this.dialogRef.close();
  }
}

export function openEditAccountDialog(
  dialog: MatDialog,
  account: Account,
  accountList: Account[],
  isAdd: boolean
) {
  const config = new MatDialogConfig();

  config.disableClose = true;
  config.autoFocus = true;
  config.panelClass = 'modal-panel';
  config.backdropClass = 'backdrop-modal-panel';

  let modalData = {
    account: account,
    accountList: accountList,
    isAdd: isAdd,
  };
  config.data = {
    ...modalData,
  };

  const dialogRef = dialog.open(AccountEditComponent, config);

  return dialogRef.afterClosed();
}
