import { Component } from '@angular/core';
import { Configuration } from './_helpers/Configuration';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'ShopAdmin';
  a:object | undefined = Configuration.getConfiguration("Url");
  ok(){
    console.log(this.a);
  }
}
