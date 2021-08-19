import {Component} from '@angular/core';
import {AccountRepositoryService} from '../services/api/account-repository.service';
import {TokenService} from '../services/tokens/token.service';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {CookieService} from 'ngx-cookie-service';
import {Observer} from 'rxjs';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: [],
    providers: [AccountRepositoryService, HttpClient, TokenService, CookieService]
})
export class LoginComponent {
    loginOrEmail: string = '';
    password: string = '';
    errorMessage: string = ''

    constructor(
        private _accounts: AccountRepositoryService,
        private _token: TokenService,
        private _route: ActivatedRoute,
        private _router: Router
    ) {
    }

    login(): void {
        if (this.loginOrEmail == '') {
            this.errorMessage = 'Enter your username or email';
            return;
        }
        if (this.password == '') {
            this.errorMessage = 'Enter your password';
            return;
        }
        let observer: Observer<any> = {
            error: (response: HttpErrorResponse) => {
                this.loginErrorHandle(response);
                console.log(response);
            },
            next: (next: any) => {
                this.jwtTokenSave(next.jwtToken);
                console.log(next);
            },
            complete: () => {
                this.errorMessage = "";
                this.navigateToPreviousPage();
            }
        }
        this._accounts.login(this.loginOrEmail, this.password)
            .subscribe(observer);
    }

    private loginErrorHandle(response: HttpErrorResponse): void {
        if (response.status == 400) {
            this.errorMessage = response.error.message;
        }
    }

    private jwtTokenSave(jwtToken: string): void {
        this._token.jwtToken = jwtToken;
    }

    private navigateToPreviousPage(): void {
        const redirect = window.history.state.redirect ?? 'search';
        this._router.navigate([redirect]);
    }
}