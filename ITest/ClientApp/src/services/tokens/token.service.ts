import {Injectable} from "@angular/core";
import {CookieService} from "ngx-cookie-service";

@Injectable()
export class TokenService {
    constructor(
        private _cookie: CookieService
    ) {
    }
    
    isJwtTokenExists() {
        return this._cookie.check("ITest_jwtToken");
    }

    get jwtToken(): string {
        return this._cookie.get("ITest_jwtToken");
    }

    set jwtToken(value: string) {
        this._cookie.set("ITest_jwtToken", value);
    }
}