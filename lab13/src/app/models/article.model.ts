export type CategoryType = 'Owoce' | 'Warzywa' | 'Nabiał' | 'Inne';

export interface Article {
  id?: number;
  title: string;
  price: number;
  categoryName: CategoryType;
  imageName?: string | null; 
}