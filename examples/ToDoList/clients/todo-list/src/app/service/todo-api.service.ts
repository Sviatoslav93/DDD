import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ToDoListsResponse} from "../contracts/responses/todo-lists-response";
import { environment } from '../../environments/environment';
import {ToDoList} from "../models/todo-list";
import { ToDoItem } from '../models/to-do-item';

@Injectable({
  providedIn: 'root'
})
export class TodoApiService {

  private readonly todoListBaseUrl = 'api/todoList';

  constructor(private http: HttpClient) { }

  getTodoLists(pageNumber: number, pageSize: number){
    const url = `${environment.apis.todoApi}/${this.todoListBaseUrl}?pageNumber=${pageNumber}&pageSize=${pageSize}`;
    return this.http.get<ToDoListsResponse>(url);
  }

  GetToDoList(id: string) {
    const url = `${environment.apis.todoApi}/${this.todoListBaseUrl}/${id}`;
    return this.http.get<ToDoList>(url);
  }
  
  AddToDoItem(id: number, todoItem: ToDoItem){
    const url = `${environment.apis.todoApi}/${this.todoListBaseUrl}/${id}`;
    return this.http.post(url, todoItem);
  }
}
