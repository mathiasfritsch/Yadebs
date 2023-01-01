import { Component, OnInit } from '@angular/core';
export interface DialogData {
  animal: string;
  name: string;
}

import {
  MatDialog,
  MAT_DIALOG_DATA,
  MatDialogRef,
} from '@angular/material/dialog';
@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  constructor(public dialog: MatDialog) {}
  ngOnInit(): void {}

}
