import { Component } from '@angular/core';
import { element } from 'protractor'
import items from './../assets/Items.json';  

  interface NavigationModel {
    Name: string;
    Children: NavigationModel[];
}

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
}) 

export class AppComponent { 
  public items: NavigationModel[] = items;
}
