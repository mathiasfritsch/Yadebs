import { Component, OnInit, Inject } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import {
  MatDialogRef,
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogConfig,
} from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Account } from 'src/app/shared/account';

@Component({
  selector: 'app-account-edit',
  templateUrl: './account-edit.component.html',
  styleUrls: ['./account-edit.component.css'],
})
export class AccountEditComponent implements OnInit {
  isAdd: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<AccountEditComponent>,
    private router: Router,
    @Inject(MAT_DIALOG_DATA) public account: Account
  ) {}

  accountForm = this.formBuilder.group({
    name: [this.account.name, Validators.required],
    number: [this.account.number, Validators.required],
  });

  ngOnInit(): void {}

  closeForm(): void {
    this.dialogRef.close();
    this.router.navigateByUrl('accounts/list');
  }

  submitForm(): void {
    if (this.accountForm.invalid) {
      return;
    }
    this.dialogRef.close();
    this.router.navigateByUrl('accounts/list');
  }
}

export function openEditCourseDialog(dialog: MatDialog, account: Account) {
  const config = new MatDialogConfig();

  config.disableClose = true;
  config.autoFocus = true;
  config.panelClass = 'modal-panel';
  config.backdropClass = 'backdrop-modal-panel';

  config.data = {
    ...account,
  };

  const dialogRef = dialog.open(AccountEditComponent, config);

  return dialogRef.afterClosed();
}
