import {Component, OnInit} from '@angular/core';
import {Test} from '../models/tests/test.model';
import {TokenService} from '../services/tokens/token.service';
import {Router} from '@angular/router';
import {Question} from "../models/tests/question/question.model";
import {Choice} from "../models/tests/choices/choice.model";
import {QuestionType} from "../models/tests/question/question-type.enum";
import {HttpClient, HttpErrorResponse, HttpResponse} from "@angular/common/http";
import {TestRepositoryService} from "../services/api/test-repository.service";
import {Observer} from "rxjs";

@Component({
    selector: 'creator',
    templateUrl: './creator.component.html',
    styleUrls: [],
    providers: [TokenService, TestRepositoryService, HttpClient]
})
export class CreatorComponent implements OnInit {
    questions: Array<Question> = new Array<Question>();
    test: Test = new Test();

    constructor(private _accounts: TestRepositoryService,
                private _token: TokenService,
                private _router: Router) {
        this.test.questions = new Array<Question>();
    }

    ngOnInit(): void {
        if (!this._token.isJwtTokenExists()) {
            const redirectState = {state: {redirect: this._router.url}};
            this._router.navigate(['/login'], redirectState);
        }
    }

    addQuestion(): void {
        if (this.questions.length > 50) {
            return;
        }
        const newQuestion = new Question();
        newQuestion.questionString = '';
        newQuestion.questionType = QuestionType.Text;

        //const newChoice = new Choice();
        //newChoice.choiceString = '';

        newQuestion.choices = new Array<Choice>();

        //newQuestion.choices.push(newChoice);
        this.test.questions.push(newQuestion);
    }

    addChoice(question: Question): void {
        if (question.choices.length > 50) {
            return;
        }
        const newChoice = new Choice();
        newChoice.choiceString = '';
        question.choices.push(newChoice);
    }

    removeChoice(question: Question, choiceIndex: number) {
        question.choices.splice(choiceIndex, 1);
    }

    removeQuestion(questionIndex: number): void {
        this.test.questions.splice(questionIndex, 1);
    }

    onSelectQuestionTypeChange($event: Event, question: Question) {
        const select = $event.target as HTMLSelectElement;
        const selectValue = Number.parseInt(select.value);
        question.questionType = selectValue;
        if (selectValue == QuestionType.Text) {
            question.choices.length = 0;
        } else if (question.choices.length == 0) {
            const newChoice = new Choice();
            newChoice.choiceString = '';
            question.choices.push(newChoice);
        }
    }

    createTest() {
        let observer: Observer<any> = {
            error: (response: HttpErrorResponse) => {
                console.log(response);
            },
            next: (next: HttpResponse<Test>) => {
                this.test.id = next.body.id;
                console.log(next.url);
                console.log(next.body);
            },
            complete: () => {
                const testPreviewUrl = `'/test-preview'${this.test.id}`;
                this._router.navigate([testPreviewUrl]);
            }
        }
        console.log(this.test);
        this._accounts.createTest(this.test).subscribe(observer)
    }
}