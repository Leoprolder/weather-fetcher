import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'weather-info',
    templateUrl: 'weather-info.component.html',
    styleUrls: [ 'weather-info.component.scss' ]
})

export class WeatherInfoComponent implements OnInit {
    public weather: any = null;

    constructor() { }

    ngOnInit() { }

    public loadWeather(weather: any): void {
        this.weather = weather;
    }
}