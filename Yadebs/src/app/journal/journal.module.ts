import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { JournalListComponent } from './journal-list/journal-list.component';
import { JounrnalRoutingModule } from './journal-routing.module';
import * as journalReducer from './store/journal.reducer';
import { JournalEffects } from './store/journal.effects';
import { EffectsModule } from '@ngrx/effects';
import { MaterialModule } from './../shared/material.module';
import { StoreModule } from '@ngrx/store';
import { JournalEditComponent } from './journal-edit/journal-edit.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MAT_DATE_LOCALE, MAT_DATE_FORMATS } from '@angular/material/core';

const DE_DATE_FORMAT = {
  parse: {
    dateInput: 'DD.MM.YYYY',
  },
  display: {
    dateInput: 'DD.MM.YYYY',
    monthYearLabel: 'MMMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY',
  },
};

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
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [
    { provide: MAT_DATE_FORMATS, useValue: DE_DATE_FORMAT },
    { provide: MAT_DATE_LOCALE, useValue: 'de-DE' },
  ],
})
export class JournalModule {}
