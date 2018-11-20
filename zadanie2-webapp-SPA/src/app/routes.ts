import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { TerminyComponent } from './terminy/terminy.component';
import { NovinkyComponent } from './novinky/novinky.component';


export const appRoutes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'terminy', component: TerminyComponent },
    { path: 'novinky', component: NovinkyComponent }
];
