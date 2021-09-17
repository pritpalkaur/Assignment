import { Injectable } from '@angular/core';
import { Book } from './book.model';
import { HttpClient } from "@angular/common/http";

@Injectable({
  providedIn: 'root'
})
export class BookService {

  constructor(private http: HttpClient) { }

  readonly baseURL = 'http://localhost:54277/api/Book'
  formData: Book = new Book();
  list: Book[];

  postBook() {
    return this.http.post(this.baseURL, this.formData);
  }

  putBook() {
    return this.http.put(`${this.baseURL}/${this.formData.Id}`, this.formData);
  }

  deletePaymentDetail(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }

  refreshList() {
    this.http.get(this.baseURL)
      .toPromise()
      .then(res =>this.list = res as Book[]);
  }


}
