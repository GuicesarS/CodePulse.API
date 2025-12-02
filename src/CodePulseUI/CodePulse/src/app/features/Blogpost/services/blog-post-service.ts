import { inject, Injectable } from '@angular/core';
import { AddBlogPostRequest, BlogPost } from '../models/blogpost.model';
import { HttpClient, httpResource, HttpResourceRef } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BlogPostService {

  private http = inject(HttpClient);
  private apiBaseUrl = environment.apiBaseUrl;
  
  createBlogPost(data: AddBlogPostRequest): Observable<BlogPost>
  {
    return this.http.post<BlogPost>(`${this.apiBaseUrl}/api/BlogPost`, data)
  }

  getAllBlogPosts(): HttpResourceRef<BlogPost[] | undefined>
  {
    return httpResource<BlogPost[]>( ()=> `${this.apiBaseUrl}/api/BlogPost`)
  }
}
