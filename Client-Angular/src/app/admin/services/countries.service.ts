import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Country } from '../models/country';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CountriesService {

  private _apiUrl: string = 'http://localhost:5091/api/CountriesAPI';

  constructor(private httpClient: HttpClient) {
  }

  getCountry(countryId: number): Observable<Country> {
    return this.httpClient.get<Country>(this._apiUrl + '/' + countryId, { responseType: 'json' });
  }


  getCountries(): Observable<Country[]> {
    return this.httpClient.get<Country[]>(this._apiUrl, { responseType: 'json' })
      .pipe(map(
        (data: Country[]) => {
          // debugger;          
          return data;
        }
      ));
  }


  addCountry(countryToAdd: Country): Observable<Country> {
    return this.httpClient.post<Country>(this._apiUrl, countryToAdd, { responseType: 'json' });
  }

  editCountry(countryToEdit: Country): Observable<Country> {
    return this.httpClient.put<Country>(this._apiUrl + '/' + countryToEdit.countryId, countryToEdit, { responseType: 'json' });
  }

  deleteCountry(countryId: number): Observable<string> {
    return this.httpClient.delete<string>(this._apiUrl + '/' + countryId, { responseType: 'json' });
  }

  searchCountries(searchBy: string, searchText: string): Observable<Country[]> {
    let url = this._apiUrl + '/' + searchBy + '/' + searchText;
    return this.httpClient.get<Country[]>(url, { responseType: 'json' });
  }
}
