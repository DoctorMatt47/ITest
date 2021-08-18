import {Component} from '@angular/core';
import {AccountService} from "../services/api/account.service";
import {TokenService} from "../services/tokens/token.service";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {CookieService} from 'ngx-cookie-service';
import {Observer} from "rxjs";
import {Router} from "@angular/router";

@Component({
    selector: 'login',
    templateUrl: './login.component.html',
    styleUrls: [],
    providers: [AccountService, HttpClient, TokenService, CookieService]
})
export class LoginComponent {
    loginOrEmail: string = "";
    password: string = "";
    errorMessage: string = ""
    continueRoot: string = "/search";

    constructor(
        private _accounts: AccountService,
        private _token: TokenService,
        private _router: Router
    ) {
    }

    login(): void {
        if (this.loginOrEmail == "") {
            this.errorMessage = "Enter your username or email";
            return;
        }
        if (this.password == "") {
            this.errorMessage = "Enter your password";
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
                this._router.navigate([this.continueRoot]);
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
}