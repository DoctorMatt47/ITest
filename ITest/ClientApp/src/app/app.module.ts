﻿import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';

import { SearchComponent } from '../search/search.component';
import { CreatorComponent } from '../creator/creator.component';
import { TestComponent } from '../test/test.component';
import { LoginComponent } from '../login/login.component';
import { SignUpComponent } from '../signup/signup.component';
import { NotFoundComponent } from '../notfound/notfound.component';

const appRoutes: Routes =[
    { path: '', component: AppComponent},
    { path: 'search', component: SearchComponent},
    { path: 'creator', component: CreatorComponent},
    { path: 'test', component: TestComponent},
    { path: 'login', component: LoginComponent},
    { path: 'signup', component: SignUpComponent},
    { path: '**', component: NotFoundComponent }
];

@NgModule({
    imports: [BrowserModule, FormsModule, RouterModule.forRoot(appRoutes)],
    declarations: [AppComponent, SearchComponent, CreatorComponent],
    bootstrap: [AppComponent]
})
export class AppModule { }