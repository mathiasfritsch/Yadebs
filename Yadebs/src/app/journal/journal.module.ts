import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JournalListComponent } from './journal-list/journal-list.component';
import { JounrnalRoutingModule } from './journal-routing.module';
import * as journalReducer from './store/journal.reducer';
import { JournalEffects } from './store/journal.effects';
import { EffectsModule } from '@ngrx/effects';
import { StoreModule } from '@ngrx/store';
import { MatTableModule } from '@angular/material/table';
@NgModule({
  declarations: [JournalListComponent],
  imports: [
    CommonModule,
    JounrnalRoutingModule,
    StoreModule.forFeature(
      journalReducer.journalFeatureKey,
      journalReducer.reducer
    ),
    EffectsModule.forFeature([JournalEffects]),
    MatTableModule,
  ],
})
export class JournalModule {}
