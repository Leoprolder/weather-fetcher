import { Component, Input, OnInit } from '@angular/core';
import { Weather } from 'src/app/models/weather';

@Component({
    selector: 'query-history',
    templateUrl: 'query-history.component.html',
    styleUrls: [ 'query-history.component.scss' ]
})

export class QueryHistoryComponent implements OnInit {
    @Input() public queryHistory: Weather[] | null;

    constructor() { }

    ngOnInit() {
    }
}