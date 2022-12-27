import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';
import { AccountsRoutingModule } from './accounts-routing.module';
import { AccountListComponent } from './account-list/account-list.component';
import * as fromAccount from './store/account.reducer';
import { AccountEffects } from './store/account.effects';
import { AccountEditComponent } from './account-edit/account-edit.component';
import { EffectsModule } from '@ngrx/effects';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MaterialModule } from '../shared/material.module';

@NgModule({
  declarations: [AccountListComponent, AccountEditComponent],
  imports: [
    CommonModule,
    AccountsRoutingModule,
    StoreModule.forFeature(fromAccount.accountFeatureKey, fromAccount.reducer),
    EffectsModule.forFeature([AccountEffects]),
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
  ],
})
export class AccountsModule {}
