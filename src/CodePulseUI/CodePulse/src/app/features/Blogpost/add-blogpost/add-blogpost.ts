import { Component, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { BlogPostService } from '../services/blog-post-service';
import { AddBlogPostRequest } from '../models/blogpost.model';
import { Router } from '@angular/router';


@Component({
  selector: 'app-add-blogpost',
  imports: [ReactiveFormsModule],
  templateUrl: './add-blogpost.html',
  styleUrl: './add-blogpost.css',
})
export class AddBlogpost {

  blogPostService = inject(BlogPostService);
  router = inject(Router);

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

    isVisible: new FormControl<boolean>(true, {
      nonNullable: true,
    }),

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
      isVisible: formRawValue.isVisible
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
