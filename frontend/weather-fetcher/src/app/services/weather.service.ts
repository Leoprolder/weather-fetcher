import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable, switchMap } from "rxjs";
import { environment } from "src/environments/environment";
import { RequestParameters } from "../constants/request-parameters.contants";
import { Weather } from "../models/weather";

@Injectable({
    providedIn: 'root'
})
export class WeatherService {
    constructor(private _httpClient: HttpClient) {
    }

    public getWeatherForecastByZip(zip: string): Observable<Weather> {
        let params = new HttpParams().set("zip", zip)
        return this._httpClient.get<Weather>(`${environment.apiUrl}/weather`, { params })
    }
}