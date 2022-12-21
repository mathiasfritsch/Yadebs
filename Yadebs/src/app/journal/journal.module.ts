import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JournalListComponent } from './journal-list/journal-list.component';
import { JounrnalRoutingModule } from './journal-routing.module';
import * as journalReducer from './store/journal.reducer';
import { JournalEffects } from './store/journal.effects';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { JournalEditComponent } from './journal-edit/journal-edit.component';

@NgModule({
  declarations: [JournalListComponent, JournalEditComponent],
  imports: [
    CommonModule,
    JounrnalRoutingModule,
    StoreModule.forFeature(
      journalReducer.journalFeatureKey,
      journalReducer.reducer
    ),
    EffectsModule.forFeature([JournalEffects]),
    MatTableModule,
    MatIconModule,
    MatButtonModule,
  ],
})
export class JournalModule {}
