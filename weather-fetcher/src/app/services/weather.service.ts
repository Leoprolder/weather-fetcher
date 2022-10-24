import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable, switchMap } from "rxjs";
import { environment } from "src/environments/environment";
import { RequestParameters } from "../constants/request-parameters.contants";

@Injectable({
    providedIn: 'root'
})
export class WeatherService {
    constructor(private _httpClient: HttpClient) {
    }

    public getWeatherForecastByZip(zip: string): Observable<any> {
        return this._getCoodrinatesByZip(zip).pipe(
            switchMap(coordinates => {
                let params = new HttpParams()
                    .set(RequestParameters.lat, coordinates.lat)
                    .set(RequestParameters.lon, coordinates.lon);

                return this._httpClient.get<any>(`${environment.weatherApiUrl}`, { params });
            })
        )
    }

    private _getCoodrinatesByZip(zip: string): Observable<any> {
        let params = new HttpParams()
            .set(RequestParameters.zip, `${zip},US`)

        return this._httpClient.get<any>(
            `${environment.geocodingApiUrl}`, { params });
    }
}