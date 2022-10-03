import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StoreModule } from '@ngrx/store';
import { AccountsRoutingModule } from './accounts-routing.module';
import { AccountListComponent } from './account-list/account-list.component';
import * as fromAccount from './store/account.reducer';
import { AccountEffects } from './store/account.effects';

import { EffectsModule } from '@ngrx/effects';
@NgModule({
  declarations: [AccountListComponent],
  imports: [
    CommonModule,
    AccountsRoutingModule,
    StoreModule.forFeature(fromAccount.accountFeatureKey, fromAccount.reducer),
    EffectsModule.forFeature([AccountEffects]),
  ],
})
export class AccountsModule {}
