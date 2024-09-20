export interface ToDoItem {
  id: string;
  title: string;
  description: string;
  createdDate: string;
  dueDate?: string;
  completedDate?: string;
  isDone: boolean;
  isFailed: boolean;
}
