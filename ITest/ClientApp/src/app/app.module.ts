import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {FormsModule} from '@angular/forms';
import {Router, RouterModule, Routes} from '@angular/router';
import {AppComponent} from './app.component';
import {HttpClientModule} from "@angular/common/http";
import {CookieService} from 'ngx-cookie-service';

import {SearchComponent} from '../search/search.component';
import {CreatorComponent} from '../creator/creator.component';
import {TestComponent} from '../test/test.component';
import {LoginComponent} from '../login/login.component';
import {SignUpComponent} from '../signup/signup.component';
import {NotFoundComponent} from '../not-found/not-found.component';
import {TestPreviewComponent} from "../test-preview/test-preview.component";

const appRoutes: Routes = [
    {path: '', redirectTo: "search", pathMatch: "full"},
    {path: 'search', component: SearchComponent},
    {path: 'creator', component: CreatorComponent},
    {path: 'test/:id', component: TestComponent},
    {path: 'test-preview/:id', component: TestPreviewComponent},
    {path: 'login', component: LoginComponent},
    {path: 'signup', component: SignUpComponent},
    {path: '**', component: NotFoundComponent}
];

@NgModule({
    imports: [BrowserModule, FormsModule, HttpClientModule, RouterModule.forRoot(appRoutes)],
    declarations: [
        AppComponent, SearchComponent, CreatorComponent, TestComponent,
        LoginComponent, SignUpComponent, NotFoundComponent, TestPreviewComponent
    ],
    providers: [CookieService],
    bootstrap: [AppComponent]
})
export class AppModule {
}