import {Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {TokenService} from "../tokens/token.service";
import {Test} from "../../models/tests/test.model";

@Injectable()
export class TestQuestionsChoicesService {
    private readonly _url = "api/tests-questions-choices";

    constructor(private _http: HttpClient,
                private _token: TokenService) {
    }

    get(id: string): Observable<any> {
        const getString = this._url + '/' + id;
        return this._http.get(getString);
    }

    create(test: Test): Observable<any> {
        const jwtToken = this._token.jwtToken;
        const options = {
            headers: new HttpHeaders({
                Authorization: `bearer ${jwtToken}`
            }),
            observe: 'response' as 'body'
        };
        return this._http.post(this._url, test, options);
    }
}