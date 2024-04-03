import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SeoSearchService {
  constructor(private http: HttpClient) {}

  getSearchResults(url: string, searchTerm: string): Observable<any[]> {
    const apiEndpoint = 'todo';
    return this.http.get<any[]>(
      `${apiEndpoint}?url=${encodeURIComponent(
        url
      )}&searchTerm=${encodeURIComponent(searchTerm)}`
    );
  }
}
