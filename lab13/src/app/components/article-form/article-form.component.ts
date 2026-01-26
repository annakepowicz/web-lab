// article-form.component.ts
import { Component, Output, EventEmitter, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ArticleService } from '../../services/article.service';
import { Article, CategoryType } from '../../models/article.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-article-form',
  standalone: true,
  imports: [FormsModule, CommonModule],
  template: `
    <div class="form-card">
      <h3>Dodaj nowy artykuł</h3>
      <input [(ngModel)]="newArticle.title" placeholder="Nazwa">
      <input [(ngModel)]="newArticle.price" type="number" placeholder="Cena">
      
      <select [(ngModel)]="newArticle.categoryName">
        <option *ngFor="let cat of articleService.categories" [value]="cat">{{cat}}</option>
      </select>

      <button (click)="save()">Zapisz</button>
      <button (click)="cancel.emit()">Anuluj</button>
    </div>
  `
})
export class ArticleFormComponent {
  articleService = inject(ArticleService);
  @Output() articleAdded = new EventEmitter<void>();
  @Output() cancel = new EventEmitter<void>();

  newArticle: Article = { title: '', price: 0, categoryName: 'Inne', imageName: null };

  save() {
    this.articleService.addArticle(this.newArticle);
    this.articleAdded.emit(); 
  }
}