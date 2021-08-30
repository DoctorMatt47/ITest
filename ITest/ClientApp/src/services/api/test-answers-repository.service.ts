import {Test} from "../../models/tests/test.model";
import {ActivatedRoute} from "@angular/router";
import {TestQuestionsChoicesRepositoryService} from "./test-questions-choices-repository.service";
import {TestAnswer} from "../../models/test-answers/test-answer";
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Injectable} from "@angular/core";
import {TokenService} from "../tokens/token.service";

@Injectable()
export class TestAnswersRepositoryService {
    private readonly _url = "api/answers";
    
    constructor(private _http: HttpClient,
                private _token: TokenService) {
    }
    
    create(testId: string, answers: Array<TestAnswer>) {
        const answersList = answers.filter((ans) => {
            return {
                answer: ans.answer,
                choiceId: ans.choiceId,
                questionId: ans.questionId
            };
        });
        const body = {
            answers: answersList
        };
        const jwtToken = this._token.jwtToken;
        const options = {
            headers: new HttpHeaders({
                Authorization: `bearer ${jwtToken}`
            }),
            observe: 'response' as 'body'
        };
        const createUrl = this._url + '/' + testId;
        return this._http.post(createUrl, body, options);
    }
}