import { Injectable } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, switchMap, tap } from 'rxjs/operators';
import { of } from 'rxjs';
import * as ProductActions from './product.actions';
import { ProductService } from 'src/app/shared/product.service';

@Injectable()
export class ProductEffects {
  loadProducts$ = createEffect(() => {
    return this.actions$.pipe(
      ofType(ProductActions.loadProducts),
      tap((x) => {
        console.log('loadProducts called');
      }),
      switchMap(() =>
        this.ps.getProducts().pipe(
          map((books) => ProductActions.loadProductsSuccess({ data: books })),
          catchError((error) =>
            of(ProductActions.loadProductsFailure({ error }))
          )
        )
      )
    );
  });

  constructor(private actions$: Actions, private ps: ProductService) {}
}
