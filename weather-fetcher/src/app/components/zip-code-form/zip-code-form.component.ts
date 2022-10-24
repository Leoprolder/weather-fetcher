import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { WeatherService } from 'src/app/services/weather.service';

@Component({
    selector: 'zip-code-form',
    templateUrl: 'zip-code-form.component.html',
    styleUrls: [ 'zip-code-form.component.scss' ],
    providers: [ WeatherService ]
})

export class ZipCodeFormComponent implements OnInit {
    @Output() onLoadWeather = new EventEmitter();
    public form: FormGroup = new FormGroup({
        'zip': new FormControl("", [Validators.required, Validators.pattern(/^[0-9]{5}(?:-[0-9]{4})?$/)])
    });
    public waitForWeatherLoad = false;

    constructor(private _weatherService: WeatherService) { }

    ngOnInit() { }

    public getWeather(): void {
        this.waitForWeatherLoad = true;
        this._weatherService.getWeatherForecastByZip(this.form.controls['zip'].value)
            .subscribe({
                next: (weather) => this.onLoadWeather.emit(weather),
                complete: () => this.waitForWeatherLoad = false
            });
    }
}