import {QuestionType} from "./question-type.enum";
import {Choice} from "../choices/choice.model";

export class Question {
    questionString?: string;
    questionType?: QuestionType;
    choices?: Array<Choice>;
}