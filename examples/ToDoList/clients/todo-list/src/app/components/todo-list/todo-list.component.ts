import {Component} from '@angular/core';
import {ToDoList} from '../../models/todo-list';
import {TodoApiService} from "../../service/todo-api.service";
import {first} from "rxjs";
import {MatCard, MatCardContent} from "@angular/material/card";
import {MatList, MatListItem, MatListItemMeta} from "@angular/material/list";
import {MatButton, MatIconButton} from "@angular/material/button";
import {ToDoItem} from "../../models/to-do-item";
import {MatIcon} from "@angular/material/icon";

@Component({
  selector: 'app-todo-list',
  standalone: true,
  templateUrl: './todo-list.component.html',
  imports: [
    MatCard,
    MatCardContent,
    MatList,
    MatListItem,
    MatButton,
    MatIconButton,
    MatIcon,
    MatListItemMeta
  ],
  styleUrl: './todo-list.component.scss'
})
export class TodoListComponent {
  todoLists: ToDoList[] = [];
  currentPage = 1;
  currentPageSize = 10;
  currentTodos: ToDoItem[] = [];

  constructor(private todoApi: TodoApiService) {
  }

  ngOnInit() {
    this.todoApi.getTodoLists(this.currentPage, this.currentPageSize)
      .pipe(first())
      .subscribe(response => this.todoLists = response.items);
  }

  showInfo(item: ToDoList) {
    this.todoApi.GetToDoList(item.id)
      .pipe(first())
      .subscribe(response => this.currentTodos = response.items);
  }

  completeItem(id: string) {

  }

  getCompletedCount() {
    return this.currentTodos.filter(item => item.isDone).length;
  }
}
