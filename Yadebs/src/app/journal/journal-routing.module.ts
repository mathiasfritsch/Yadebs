import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { JournalListComponent } from './journal-list/journal-list.component';
import { JournalEditComponent } from './journal-edit/journal-edit.component';
const routes: Routes = [
  {
    path: 'list/:id',
    component: JournalEditComponent,
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
