import {Component, OnInit} from "@angular/core";
import {Test} from "../models/tests/test.model";
import {Observer} from "rxjs";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {TestRepositoryService} from "../services/api/test-repository.service";
import {TokenService} from "../services/tokens/token.service";

@Component({
    selector: 'test-preview',
    templateUrl: './test-preview.component.html',
    styleUrls: ['./test-preview.component.scss'],
    providers: [TokenService, TestRepositoryService, HttpClient]
})
export class TestPreviewComponent implements OnInit {
    test: Test = new Test();
    countOfQuestions: number = 0;
    isYourTest: boolean = false;

    constructor(private _route: ActivatedRoute,
                private _router: Router,
                private _token: TokenService,
                private _tests: TestRepositoryService) {
    }

    ngOnInit(): void {
        this.test.id = this._route.snapshot.params['id'];
        let observer: Observer<any> = {
            error: (response: HttpErrorResponse) => {
                console.log(response);
            },
            next: (next: Test) => {
                this.test = next;
            },
            complete: () => {
            }
        }
        this._tests.get(this.test.id).subscribe(observer);
    }

    passTest(): void {
        if (!this._token.isJwtTokenExists()) {
            const redirectState = {state: {redirect: this._router.url}};
            this._router.navigate(['/login'], redirectState);
        } else {
            this._router.navigate([`./test/${this.test.id}`]);
        }
    }

}