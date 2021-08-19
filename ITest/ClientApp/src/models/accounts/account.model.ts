import {BaseModel} from "../base.model";
import {AccountRole} from "./account-role.enum";

export class Account extends BaseModel {
    public login? : string;
    public password? : string;
    public mail? : string;
    public city? : string;
    public role? : AccountRole;
    public isConfirmed? : boolean;
}