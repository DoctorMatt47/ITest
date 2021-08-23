import {Question} from "./question/question.model";
import {BaseModel} from "../base.model";

export class Test extends BaseModel{
    title?: string;
    description?: string;
    visitorsCount?: number;
    questions?: Array<Question>;
}