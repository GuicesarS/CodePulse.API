import { Component, inject } from '@angular/core';
import { BlogPostService } from '../../Blogpost/services/blog-post-service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [RouterLink],
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home {
blogPost = inject(BlogPostService);

blogPostsRef = this.blogPost.getAllBlogPosts();
isLoading = this.blogPostsRef.isLoading;
blogPostResponse = this.blogPostsRef.value;
}
