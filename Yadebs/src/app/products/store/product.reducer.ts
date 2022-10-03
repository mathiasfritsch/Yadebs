import { Action, createReducer, on, createFeatureSelector } from '@ngrx/store';
import * as ProductActions from './product.actions';
import { Product } from 'src/app/shared/product';
import { state } from '@angular/animations';
export const productFeatureKey = 'product';
import { EntityState, EntityAdapter, createEntityAdapter } from '@ngrx/entity';
import { createSelector } from '@ngrx/store';

export const adapter: EntityAdapter<Product> = createEntityAdapter<Product>({});

export interface State extends EntityState<Product> {
  loading: boolean;
}
const productFeature = createFeatureSelector(productFeatureKey);

export const initialState: State = adapter.getInitialState({
  loading: false,
});

export const reducer = createReducer(
  initialState,

  on(ProductActions.loadProducts, (state) => {
    return { ...state, loading: true };
  }),
  on(ProductActions.loadProductsSuccess, (state, action) => {
    return {
      ...adapter.setAll(action.data, state),
      products: action.data,
      loading: false,
    };
  }),
  on(ProductActions.loadProductsFailure, (state, action) => {
    return { ...state, loading: false };
  })
);

export const { selectAll } = adapter.getSelectors();
