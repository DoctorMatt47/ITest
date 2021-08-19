import {Injectable} from "@angular/core";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Account} from "../../models/accounts/account.model";

@Injectable()
export class AccountRepositoryService {
    private readonly _url = "/api/accounts";

    constructor(private _http: HttpClient) {
    }

    login(loginOrEmail: string, password: string) {
        let body = {
            login: loginOrEmail,
            password: password
        }
        return this._http.post(this._url + '/login', body);
    }

    register(acc: Account) {
        let body = {
            login: acc.login,
            password: acc.password,
            mail: acc.mail,
            city: acc.city
        }
        return this._http.post(this._url + '/register', body);
    }
    
    delete(password: string) {
        let options = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
            }),
            body: {
                password: password
            },
        };
        return this._http.delete(this._url + 'delete', options);
    }
}