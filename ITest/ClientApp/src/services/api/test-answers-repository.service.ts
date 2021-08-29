import {Test} from "../../models/tests/test.model";
import {ActivatedRoute} from "@angular/router";
import {TestQuestionsChoicesService} from "./test-questions-choices.service";
import {TestAnswer} from "../../models/test-answers/test-answer";
import {HttpClient} from "@angular/common/http";
import {Injectable} from "@angular/core";

@Injectable()
export class TestAnswersRepositoryService {
    private readonly _url = "api/answers";
    
    constructor(private _http: HttpClient) {
    }
    
    create(testId: string, answers: Array<TestAnswer>) {
        const createUrl = this._url + '/' + testId;
        return this._http.post(createUrl, {answers});
    }
}