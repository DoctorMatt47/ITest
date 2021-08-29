import {QuestionType} from "./question-type.enum";
import {Choice} from "../choices/choice.model";
import {BaseModel} from "../../base.model";

export class Question extends BaseModel {
    questionString?: string;
    questionType?: QuestionType;
    choices?: Array<Choice>;
}