import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {TodoListComponent} from "./components/todo-list/todo-list.component";
import {MatSidenavContainer} from "@angular/material/sidenav";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    TodoListComponent,
    MatSidenavContainer,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'todo-list';
}
