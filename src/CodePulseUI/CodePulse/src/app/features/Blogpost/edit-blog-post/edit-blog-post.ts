import { Component, input } from '@angular/core';

@Component({
  selector: 'app-edit-blog-post',
  imports: [],
  templateUrl: './edit-blog-post.html',
  styleUrl: './edit-blog-post.css',
})
export class EditBlogPost {
  id = input<string>();

  
}
