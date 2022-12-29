import { Component, OnInit, Inject } from '@angular/core';
import {
  MatDialogRef,
  MAT_DIALOG_DATA,
  MatDialog,
  MatDialogConfig,
} from '@angular/material/dialog';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Store } from '@ngrx/store';
import { MaterialModule } from '../../shared/material.module';
import { Journal } from 'src/app/shared/journal';

@Component({
  selector: 'app-journal-edit',
  templateUrl: './journal-edit.component.html',
  styleUrls: ['./journal-edit.component.css'],
})
export class JournalEditComponent implements OnInit {
  constructor(
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<JournalEditComponent>,
    private router: Router,
    private store: Store,
    @Inject(MAT_DIALOG_DATA) public modalData: any
  ) {
    this.isAdd = modalData.isAdd;
    this.journal = modalData.journal;
    this.journalForm = this.formBuilder.group({
      date: [this.journal.date, Validators.required],
      name: [this.journal.name, Validators.required],
    });
  }
  isAdd: boolean = false;

  journal: Journal;
  journalForm: FormGroup;

  ngOnInit(): void {}
  closeForm(): void {
    this.dialogRef.close();
    this.router.navigateByUrl('journal/list');
  }
  submitDeleteForm(): void {
    this.dialogRef.close();
  }
  submitAddForm(): void {
    this.dialogRef.close();
  }
  submitUpdateForm(): void {
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
