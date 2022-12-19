import { Injectable } from '@angular/core';
import { Product } from './product';
import { Observable, of } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  getProducts(): Observable<Product[]> {
    return this.httpClient.get<Product[]>(
      'https://localhost:7211/api/Accounts'
    );
  }
  constructor(private httpClient: HttpClient) {}
}
