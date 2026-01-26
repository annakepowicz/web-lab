// src/app/components/footer/footer.component.ts
import { Component } from '@angular/core';

@Component({
  selector: 'app-footer',
  standalone: true,
  template: `
    <footer>
      <hr />
      <div class="footer-content">
        <p>&copy; 2024 - Laboratorium 13 - Aplikacje Webowe .NET</p>
      </div>
    </footer>
  `,
  styles: [`
    footer {
      margin-top: 40px;
      padding: 20px 0;
      color: #666;
      font-size: 0.9rem;
      text-align: center;
    }
    hr {
      border: 0;
      border-top: 1px solid #eee;
      margin-bottom: 20px;
    }
    .footer-content p {
      margin: 5px 0;
    }
  `]
})
export class FooterComponent {}