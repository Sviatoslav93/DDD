import {ToDoList} from "../../models/todo-list";

export interface ToDoListsResponse {
  itemsCount: number;
  pagesCount: number;
  pageSize: number;
  pageNumber: number;
  items: ToDoList[];
}
