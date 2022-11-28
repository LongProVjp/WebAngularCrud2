import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ProvinceComponent } from './province/province.component';
import { ProvinceListComponent } from './province-list/province-list.component';

const routes: Routes = [
  {path:'province',component:ProvinceComponent},
  {path:'provincelist',component:ProvinceListComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
