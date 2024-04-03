import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SeoSearchService } from '../../services/seo-search-service';
import { NgIf, NgFor } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatAccordion } from '@angular/material/expansion';
import { MatExpansionModule } from '@angular/material/expansion';

@Component({
  selector: 'app-search-form',
  templateUrl: './search-form.component.html',
  styleUrls: ['./search-form.component.css'],
  standalone: true,
  imports: [
    FormsModule,
    NgIf,
    NgFor,
    HttpClientModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatAccordion,
    MatExpansionModule,
  ],
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
