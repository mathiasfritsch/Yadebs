import { Component, OnInit } from '@angular/core';
import { Store, select } from '@ngrx/store';
import { loadProducts } from '../store/product.actions';
import {
  selectProductState,
  selectAllProducts,
} from '../store/product.selectors';

@Component({
  selector: 'bm-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css'],
})
export class ProductListComponent implements OnInit {
  products$ = this.store.pipe(select(selectAllProducts));
  loading$ = this.store.pipe(select(selectProductState));

  constructor(private store: Store) {}
  ngOnInit(): void {
    this.store.dispatch(loadProducts());
  }
}
