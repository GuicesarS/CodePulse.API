import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { BlogPostService } from '../services/blog-post-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-blogpost-list',
  imports: [RouterLink, CommonModule],
  templateUrl: './blogpost-list.html',
  styleUrl: './blogpost-list.css',
})
export class BlogpostList {
  blogPostService = inject(BlogPostService);

  getAllBlogPostRef = this.blogPostService.getAllBlogPosts();

  isLoading = this.getAllBlogPostRef.isLoading;
  error = this.getAllBlogPostRef.error;
  response = this.getAllBlogPostRef.value;
  

  
  
}
