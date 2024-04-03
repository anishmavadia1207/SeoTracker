import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class SeoSearchService {
  constructor(private http: HttpClient) {}

  getSearchResults(url: string, searchTerm: string): Observable<any[]> {
    const apiEndpoint = environment.apiUrl + 'rank';
    return this.http.get<any[]>(
      `${apiEndpoint}?url=${encodeURIComponent(
        url
      )}&searchTerm=${encodeURIComponent(searchTerm)}`
    );
  }
}
