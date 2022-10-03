import { createFeatureSelector, createSelector } from '@ngrx/store';
import * as fromProduct from './product.reducer';

export const selectProductState = createFeatureSelector<fromProduct.State>(
  fromProduct.productFeatureKey
);

export const selectSelectorsLoading = createSelector(
  selectProductState,
  (state) => state.loading
);

export const selectAllProducts = createSelector(
  selectProductState,
  fromProduct.selectAll
);
export const selectProduct = createSelector(
  selectProductState,
  (state) => state.entities[1]
);
