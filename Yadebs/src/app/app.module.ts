import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { StoreModule } from '@ngrx/store';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { EffectsModule } from '@ngrx/effects';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { MaterialModule } from './shared/material.module';

import { LayoutModule } from '@angular/cdk/layout';
import {
  MomentDateAdapter,
  MAT_MOMENT_DATE_ADAPTER_OPTIONS,
} from '@angular/material-moment-adapter';
import { reducers } from './store';
import { AccountEffects } from './store/account/account.effects';
import { JournalEffects } from './store/journal/journal.effects';

import { JournalEditComponent } from './journal/journal-edit/journal-edit.component';
import { JournalListComponent } from './journal/journal-list/journal-list.component';
import { AccountListComponent } from './accounts/account-list/account-list.component';
import { AccountEditComponent } from './accounts/account-edit/account-edit.component';
import {
  DateAdapter,
  MAT_DATE_LOCALE,
  MAT_DATE_FORMATS,
} from '@angular/material/core';

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
  declarations: [
    AppComponent,
    HomeComponent,
    JournalListComponent,
    JournalEditComponent,
    AccountListComponent,
    AccountEditComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    StoreModule.forRoot(reducers),
    StoreDevtoolsModule.instrument({
      name:'yadebs store',
      maxAge: 25,
    }),
    EffectsModule.forRoot([AccountEffects, JournalEffects]),
    HttpClientModule,
    BrowserAnimationsModule,

    ReactiveFormsModule,
    FormsModule,
    MaterialModule,
    LayoutModule,
  ],
  providers: [
    {
      provide: DateAdapter,
      useClass: MomentDateAdapter,
      deps: [MAT_DATE_LOCALE, MAT_MOMENT_DATE_ADAPTER_OPTIONS],
    },

    { provide: MAT_DATE_FORMATS, useValue: DE_DATE_FORMAT },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
