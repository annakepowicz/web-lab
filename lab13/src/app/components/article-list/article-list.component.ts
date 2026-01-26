// article-list.component.ts
import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ArticleService } from '../../services/article.service';

@Component({
  selector: 'app-article-list',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="list-container">
      <h2>Lista Artykułów</h2>
      <ul>
        <li *ngFor="let art of articleService.articles()">
          <strong>{{ art.title }}</strong> — Kategoria: {{ art.categoryName }}
          <button (click)="articleService.deleteArticle(art.id!)">Usuń</button>
        </li>
      </ul>
    </div>
  `
})
export class ArticleListComponent {
  public articleService = inject(ArticleService);
}