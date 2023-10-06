import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Country } from '../models/country';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ApiPaths, environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class CountriesService {

  private url: string;

  constructor(private httpClient: HttpClient) {
    this.url = `${environment.baseUrl}/${ApiPaths.Countries}`;
  }

  getCountry(countryId: number): Observable<Country> {
    return this.httpClient.get<Country>(this.url + '/' + countryId, { responseType: 'json' });
  }


  getCountries(): Observable<Country[]> {
    return this.httpClient.get<Country[]>(this.url, { responseType: 'json' })
      .pipe(map(
        (data: Country[]) => {
          // debugger;          
          return data;
        }
      ));
  }


  addCountry(countryToAdd: Country): Observable<Country> {
    return this.httpClient.post<Country>(this.url, countryToAdd, { responseType: 'json' });
  }

  editCountry(countryToEdit: Country): Observable<Country> {
    return this.httpClient.put<Country>(this.url + '/' + countryToEdit.countryId, countryToEdit, { responseType: 'json' });
  }

  deleteCountry(countryId: number): Observable<string> {
    return this.httpClient.delete<string>(this.url + '/' + countryId, { responseType: 'json' });
  }

  searchCountries(searchBy: string, searchText: string): Observable<Country[]> {
    let fullUrl =`${this.url}/${searchBy}/${searchText}`;
    return this.httpClient.get<Country[]>(fullUrl, { responseType: 'json' });
  }
}
