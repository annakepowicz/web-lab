import { Injectable, signal, computed } from '@angular/core';
import { Article, CategoryType } from '../models/article.model';

@Injectable({ providedIn: 'root' })
export class ArticleService {
  // Prywatny sygnał ze stanem artykułów
  private articlesSignal = signal<Article[]>([
    { id: 1, title: 'Pomidor', price: 5.50, categoryName: 'Warzywa', imageName: null },
    { id: 2, title: 'Mleko', price: 3.20, categoryName: 'Nabiał', imageName: null }
  ]);

  // Publiczne sygnały do odczytu
  articles = computed(() => this.articlesSignal());
  categories: CategoryType[] = ['Owoce', 'Warzywa', 'Nabiał', 'Inne']; // 

  addArticle(article: Article) {
    const newArticle = { ...article, id: Date.now() };
    this.articlesSignal.update(list => [...list, newArticle]); 
  }

  deleteArticle(id: number) {
    this.articlesSignal.update(list => list.filter(a => a.id !== id));
  }
}