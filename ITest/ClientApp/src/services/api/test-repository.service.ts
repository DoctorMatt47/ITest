import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {Test} from "../../models/tests/test.model";
import {TokenService} from "../tokens/token.service";
import {Observable} from "rxjs";

@Injectable()
export class TestRepositoryService {
    private readonly _url = "api/tests";

    constructor(private _http: HttpClient,
                private _token: TokenService) {
    }
    
    get(id: string): Observable<any> {
        const getString = this._url + '/' + id;
        return this._http.get(getString);
    }
}