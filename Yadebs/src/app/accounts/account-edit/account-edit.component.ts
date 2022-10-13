import { Component, OnInit, Input } from '@angular/core';
import {
  ReactiveFormsModule,
  FormBuilder,
  FormGroup,
  FormControl,
} from '@angular/forms';
import { FloatLabelType } from '@angular/material/form-field';
@Component({
  selector: 'app-account-edit',
  templateUrl: './account-edit.component.html',
  styleUrls: ['./account-edit.component.css'],
})
export class AccountEditComponent implements OnInit {
  constructor(private _formBuilder: FormBuilder) {}
  accountForm = new FormGroup({
    name: new FormControl(''),
    age: new FormControl(''),
  });
  ngOnInit(): void {}

  setProperty(): void {
    this.accountForm.patchValue({ name: 'some' });
  }
}
