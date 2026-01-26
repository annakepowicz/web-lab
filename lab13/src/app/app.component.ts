// src/app/app.component.ts
import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HeaderComponent } from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { ArticleListComponent } from './components/article-list/article-list.component';
import { ArticleFormComponent } from './components/article-form/article-form.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    CommonModule, 
    HeaderComponent, 
    FooterComponent, 
    ArticleListComponent, 
    ArticleFormComponent
  ],
  template: `
    <app-header></app-header>
    
    <main class="container">
      <div *ngIf="!isAdding(); else formView">
        <div class="actions">
          <button (click)="isAdding.set(true)">+ Dodaj nowy artykuł</button>
        </div>

        <app-article-list></app-article-list>
      </div>

      <ng-template #formView>
        <app-article-form 
          (articleAdded)="isAdding.set(false)" 
          (cancel)="isAdding.set(false)">
        </app-article-form>
      </ng-template>
    </main>

    <app-footer></app-footer>
  `,
  styles: [`
    .container { padding: 20px; max-width: 800px; margin: 0 auto; }
    .actions { margin-bottom: 20px; }
  `]
})
export class AppComponent {
  // Sygnał zarządzający stanem UI (lista vs formularz) 
  isAdding = signal(false);
}