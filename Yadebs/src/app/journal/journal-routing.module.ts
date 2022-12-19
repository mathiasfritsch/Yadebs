import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { JournalListComponent } from './journal-list/journal-list.component';

const routes: Routes = [
  {
    path: 'list/:id',
    component: JournalListComponent,
    pathMatch: 'full',
  },
  {
    path: 'list',
    component: JournalListComponent,
    pathMatch: 'full',
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class JounrnalRoutingModule {}
