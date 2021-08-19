import {Component} from '@angular/core';
import {AccountRepositoryService} from "../services/api/account-repository.service";
import {Account} from "../models/accounts/account.model";
import {Observer} from "rxjs";
import {HttpErrorResponse} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
    selector: 'signup',
    templateUrl: './signup.component.html',
    styleUrls: [],
    providers: [AccountRepositoryService]
})
export class SignUpComponent {
    account: Account = new Account();
    confirmPassword: string = "";
    loginPath: string = "/login"

    constructor(
        private _accounts: AccountRepositoryService,
        private _router: Router) {
    }

    signup(): void {
        if (this.confirmPassword != this.account.password) {
            return;
        }
        let observer: Observer<Account> = {
            error: (response: HttpErrorResponse) => {
                console.log(response);
            },
            next: (data: Account) => {
                console.log(data);
            },
            complete: () => {
                this._router.navigate([this.loginPath]);
            }
        }
        this._accounts.register(this.account).subscribe(observer)
    }
}