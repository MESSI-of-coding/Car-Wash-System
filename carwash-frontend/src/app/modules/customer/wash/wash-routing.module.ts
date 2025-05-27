import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { WashNowComponent } from './components/wash-now/wash-now.component';
import { WashRequestsComponent } from './components/wash-requests/wash-requests.component';
import { MyRequestsComponent } from './components/my-requests/my-requests.component';

const routes: Routes = [
  { path: 'now', component: WashNowComponent },
  { path: 'requests', component: WashRequestsComponent },
  { path: 'requests', component: MyRequestsComponent },

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class WashRoutingModule { }
