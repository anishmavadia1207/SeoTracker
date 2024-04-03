import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SeoSearchService } from '../../services/seo-search-service';
import { NgIf, NgFor } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-search-form',
  templateUrl: './search-form.component.html',
  styleUrls: ['./search-form.component.css'],
  standalone: true,
  imports: [FormsModule, NgIf, NgFor, HttpClientModule], // Standalone component imports
})
export class SearchFormComponent {
  searchParams = {
    url: '',
    searchTerm: '',
  };
  results: any[] | null = null;

  constructor(private seoSearchService: SeoSearchService) {}

  onSubmit(): void {
    this.seoSearchService
      .getSearchResults(this.searchParams.url, this.searchParams.searchTerm)
      .subscribe((results) => (this.results = results));
  }
}
