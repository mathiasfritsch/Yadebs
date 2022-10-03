import { createAction, props } from '@ngrx/store';
import { Product } from 'src/app/shared/product';
import { HttpErrorResponse } from '@angular/common/http';

export const loadProducts = createAction('[Product] Load Products');

export const loadProductsSuccess = createAction(
  '[Product] Load Products Success',
  props<{ data: Product[] }>()
);

export const loadProductsFailure = createAction(
  '[Product] Load Products Failure',
  props<{ error: HttpErrorResponse }>()
);
