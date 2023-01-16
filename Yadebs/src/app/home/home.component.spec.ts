import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HomeComponent } from './home.component';
import { getTestScheduler, cold } from 'jasmine-marbles';
import { map } from 'rxjs/operators';
import { merge } from 'rxjs';
let component: HomeComponent;
let fixture: ComponentFixture<HomeComponent>;
let h1: HTMLElement;

beforeEach(() => {
  TestBed.configureTestingModule({
    imports: [ReactiveFormsModule, FormsModule],
    declarations: [HomeComponent],
  });
  fixture = TestBed.createComponent(HomeComponent);
  component = fixture.componentInstance; // BannerComponent test instance
  h1 = fixture.nativeElement.querySelector('h1');
});
it('should display a different test title', () => {
  component.title = 'Test Title';
  fixture.detectChanges();
  expect(h1.textContent).toContain('Test Title');
});
