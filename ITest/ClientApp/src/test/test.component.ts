import {Component, OnInit} from '@angular/core';
import {TokenService} from "../services/tokens/token.service";
import {TestRepositoryService} from "../services/api/test-repository.service";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {Test} from "../models/tests/test.model";
import {Observer} from "rxjs";
import {ActivatedRoute, Router} from "@angular/router";
import {TestQuestionsChoicesRepositoryService} from "../services/api/test-questions-choices-repository.service";
import {TestAnswersRepositoryService} from "../services/api/test-answers-repository.service";
import {TestAnswer} from "../models/test-answers/test-answer";

@Component({
    selector: 'test',
    templateUrl: './test.component.html',
    styleUrls: [],
    providers: [TokenService, TestQuestionsChoicesRepositoryService, TestAnswersRepositoryService, HttpClient]
})
export class TestComponent implements OnInit {
    test: Test = new Test();
    answers: Array<Ans>
    
    constructor(private _route: ActivatedRoute,
                private _tests: TestQuestionsChoicesRepositoryService,
                private _testAnswers: TestAnswersRepositoryService,
                private _router: Router) {
    }

    ngOnInit(): void {
        this.test.id = this._route.snapshot.params['id'];
        let observer: Observer<any> = {
            error: (response: HttpErrorResponse) => {
                console.log(response);
            },
            next: (next: Test) => {
                this.test = next;
                this.answers = new Array<Ans>(this.test.questions.length)
                for (let i = 0; i < this.test.questions.length; i++) {
                    this.answers[i] = new Ans();
                    this.answers[i].isChecked = 
                        new Array<boolean>(this.test.questions[i].choices.length).fill(false);
                }
                console.log(this.test);
            },
            complete: () => {
            }
        }
        this._tests.get(this.test.id).subscribe(observer);
    }
    
    submit() {
        const testAnswers = this.formAnAnswer();
        let observer: Observer<any> = {
            error: (response: HttpErrorResponse) => {
                console.log(response);
            },
            next: (next: any) => {
                console.log(next);
            },
            complete: () => {
                this._router.navigate(['/search']);
            }
        }
        this._testAnswers.create(this.test.id, testAnswers).subscribe(observer);
    }

    formAnAnswer(): Array<TestAnswer> {
        const testAnswers = new Array<TestAnswer>();
        for (let i = 0; i < this.test.questions.length; i++) {
            if (this.answers[i].isChecked.length == 0) {
                const newTestAnswer = new TestAnswer();
                newTestAnswer.answer = this.answers[i].text;
                newTestAnswer.questionId = this.test.questions[i].id;
                newTestAnswer.choiceId = null;
                testAnswers.push(newTestAnswer);
            }
            for (let j = 0; j < this.answers[i].isChecked.length; j++) {
                if (this.answers[i].isChecked[j]) {
                    const newTestAnswer = new TestAnswer();
                    newTestAnswer.answer = null;
                    newTestAnswer.questionId = this.test.questions[i].id;
                    newTestAnswer.choiceId = this.test.questions[i].choices[j].id;
                    testAnswers.push(newTestAnswer);
                }
            }
        }
        return testAnswers;
    }
    
    changeRadioButton($event, questionIndex: number, choiceIndex: number): void {
        for (let i = 0; i < this.answers[questionIndex].isChecked.length; i++) {
            this.answers[questionIndex].isChecked[i] = false;
        }
        this.answers[questionIndex].isChecked[choiceIndex] = true;
    }

    changeCheckboxButton($event, questionIndex: number, choiceIndex: number): void {
        this.answers[questionIndex].isChecked[choiceIndex] = $event.target.checked;
    }

    changeText($event, questionIndex: number): void {
        this.answers[questionIndex].text = $event.target.value;
    }
}

class Ans {
    isChecked: Array<boolean>;
    text: string = '';
}