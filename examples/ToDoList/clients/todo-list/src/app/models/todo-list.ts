import {ToDoItem} from "./to-do-item";

export interface ToDoList {
  id: string;
  title: string;
  createdBy: string;
  createdAt: string;
  updatedBy?: string;
  updatedAt?: string;
  items: ToDoItem[];
}
