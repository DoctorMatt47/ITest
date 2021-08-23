import {Component, OnInit} from '@angular/core';
import {TokenService} from "../services/tokens/token.service";
import {TestRepositoryService} from "../services/api/test-repository.service";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {Test} from "../models/tests/test.model";
import {Observer} from "rxjs";
import {ActivatedRoute} from "@angular/router";

@Component({
    selector: 'test',
    templateUrl: './test.component.html',
    styleUrls: [],
    providers: [TokenService, TestRepositoryService, HttpClient]
})
export class TestComponent implements OnInit {
    test: Test = new Test();
    
    constructor(private _route: ActivatedRoute,
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
                console.log(this.test);
            },
            complete: () => {
            }
        }
        this._tests.getTest(this.test.id).subscribe(observer);
    }
}