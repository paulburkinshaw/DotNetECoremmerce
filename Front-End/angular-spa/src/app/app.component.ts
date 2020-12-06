import { Component, Directive } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'dotnetEcoremmerce-spa';
  products: string[];

  constructor() {
    this.products = ['Product 1', 'Product 2', 'Product 3', 'Product 4'];
  }

}
