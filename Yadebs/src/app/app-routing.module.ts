import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AccountListComponent } from './accounts/account-list/account-list.component';
import { HomeComponent } from './home/home.component';
import { JournalListComponent } from './journal/journal-list/journal-list.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full',
  },
  {
    path: 'home',
    component: HomeComponent,
  },
  {
    path: 'accounts/list/:id',
    component: AccountListComponent,
    pathMatch: 'full',
  },
  {
    path: 'accounts/list',
    component: AccountListComponent,
    pathMatch: 'full',
  },
  {
    path: 'journal/list/:id',
    component: JournalListComponent,
    pathMatch: 'full',
  },
  {
    path: 'journal/list',
    component: JournalListComponent,
    pathMatch: 'full',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
