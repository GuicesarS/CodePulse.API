import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { BlogPostService } from '../services/blog-post-service';
import { AddBlogPostRequest } from '../models/blogpost.model';
import { Router } from '@angular/router';
import { MarkdownComponent } from 'ngx-markdown';
import { CommonModule } from '@angular/common';
import { CategoryService } from '../../category/services/category-service';


@Component({
  selector: 'app-add-blogpost',
  imports: [ReactiveFormsModule, MarkdownComponent, CommonModule],
  templateUrl: './add-blogpost.html',
  styleUrl: './add-blogpost.css',
})
export class AddBlogpost {

  categoryService = inject(CategoryService);
  blogPostService = inject(BlogPostService);
  router = inject(Router);

  private categoryResourceRef = this.categoryService.getAllCategories();
  categoriesResponse = this.categoryResourceRef.value; 

  addBlogPostForm = new FormGroup({
    title: new FormControl<string>('', {
      nonNullable: true,
      validators: [Validators.required, Validators.maxLength(100), Validators.minLength(10)]
    }),

    shortDescription: new FormControl<string>('', {
      nonNullable: true,
      validators: [Validators.required, Validators.maxLength(200), Validators.minLength(10)]
    }),

    content: new FormControl<string>('', {
      nonNullable: true,
      validators: [Validators.required, Validators.minLength(10)]
    }),

    featuredImageUrl: new FormControl<string>('', {
      nonNullable: true,
      validators: [Validators.required, Validators.maxLength(200)]
    }),

    urlHandle: new FormControl<string>('', {
      nonNullable: true,
      validators: [Validators.required, Validators.minLength(10)]
    }),

    publishedDate: new FormControl<string>(new Date().toISOString().split('T')[0], {
      nonNullable: true,
      validators: [Validators.required]
    }),

    author: new FormControl<string>('', {
      nonNullable: true,
      validators: [Validators.required, Validators.maxLength(100)]
    }),

    isVisible: new FormControl<boolean>(false, {
      nonNullable: true,
    }),

    categories: new FormControl<string[]>([]),

  });

  onSubmit() {
    const formRawValue = this.addBlogPostForm.getRawValue();
    const request: AddBlogPostRequest = {
      title: formRawValue.title,
      shortDescription: formRawValue.shortDescription,
      content: formRawValue.content,
      featuredImageUrl: formRawValue.featuredImageUrl,
      urlHandle: formRawValue.urlHandle,
      publishedDate: new Date(formRawValue.publishedDate),
      author: formRawValue.author,
      isVisible: formRawValue.isVisible,
      categories: formRawValue.categories ?? [],
    }
    this.blogPostService.createBlogPost(request)
    .subscribe({
      next: (response) => {
        console.log(response);
        this.router.navigate(['admin/blogposts']);
      },
      error: () => {
        console.error('Something went wrong!');
      },
    });
  }
}
