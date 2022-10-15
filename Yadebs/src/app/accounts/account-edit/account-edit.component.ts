import { Component, OnInit, Input } from '@angular/core';
import {
  ReactiveFormsModule,
  FormBuilder,
  FormGroup,
  FormControl,
  ValidationErrors,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { FloatLabelType } from '@angular/material/form-field';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';

@Component({
  selector: 'app-account-edit',
  templateUrl: './account-edit.component.html',
  styleUrls: ['./account-edit.component.css'],
})
export class AccountEditComponent implements OnInit {
  constructor(
    private _formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<AccountEditComponent>,
    private router: Router
  ) {}
  accountForm = new FormGroup({
    name: new FormControl('', Validators.required),
    number: new FormControl('', Validators.required),
  });
  ngOnInit(): void {
    console.log('init detail');
  }
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
