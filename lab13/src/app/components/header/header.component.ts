// header.component.ts
import { Component } from '@angular/core';

@Component({
  selector: 'app-header',
  standalone: true,
  template: `
    <nav class="navbar">
      <h1>Sklep Spożywczy - Panel Angular</h1>
    </nav>
  `,
  styles: ['.navbar { background: #f8f9fa; padding: 1rem; border-bottom: 1px solid #dee2e6; }']
})
export class HeaderComponent {}