import { Component, OnInit } from '@angular/core';
import { Weather } from 'src/app/models/weather';

@Component({
    selector: 'weather-info',
    templateUrl: 'weather-info.component.html',
    styleUrls: [ 'weather-info.component.scss' ]
})

export class WeatherInfoComponent implements OnInit {
    public weather: Weather;

    constructor() { }

    ngOnInit() { }

    public loadWeather(weather: any): void {
        this.weather = weather;
    }
}