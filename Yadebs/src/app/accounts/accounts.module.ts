import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccountsRoutingModule } from './accounts-routing.module';
import { AccountListComponent } from './account-list/account-list.component';
import { StoreModule } from '@ngrx/store';
import * as fromAccount from '../account.reducer';


@NgModule({
  declarations: [
    AccountListComponent
  ],
  imports: [
    CommonModule,
    AccountsRoutingModule,
    StoreModule.forFeature(fromAccount.accountsFeatureKey, fromAccount.reducer)
  ]
})
export class AccountsModule { }
