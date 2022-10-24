import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Weather } from 'src/app/models/weather';
import { WeatherService } from 'src/app/services/weather.service';

@Component({
    selector: 'weather-info',
    templateUrl: 'weather-info.component.html',
    styleUrls: [ 'weather-info.component.scss' ],
    providers: [ WeatherService ]
})

export class WeatherInfoComponent implements OnInit {
    public queries$: Observable<Weather[]>
    public weather: Weather;

    constructor(private _weatherService: WeatherService) { }

    ngOnInit() {
        this.queries$ = this._weatherService.getQueryHistory();
    }

    public loadWeather(weather: any): void {
        this.weather = weather;
        this.queries$ = this._weatherService.getQueryHistory();
    }
}