import {BaseModel} from "../basemodel";
import {AccountRole} from "./accountrole";

export class Account extends BaseModel {
    public login? : string;
    public password? : string;
    public mail? : string;
    public city? : string;
    public role? : AccountRole;
    public isConfirmed? : boolean;
}