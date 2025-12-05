import { Component, effect, inject, input } from '@angular/core';
import { BlogPostService } from '../services/blog-post-service';
import { FormGroup, FormControl, Validators, ReactiveFormsModule } from '@angular/forms';
import { MarkdownComponent } from 'ngx-markdown';
import { CategoryService } from '../../category/services/category-service';
import { UpdateBlogPostRequest } from '../models/blogpost.model';
import { Router } from '@angular/router';
import { ImageSelector } from '../../../shared/components/image-selector/image-selector';

@Component({
  selector: 'app-edit-blog-post',
  imports: [ReactiveFormsModule, MarkdownComponent, ImageSelector],
  templateUrl: './edit-blog-post.html',
  styleUrl: './edit-blog-post.css',
})
export class EditBlogPost {
  id = input<string>();
  blogPostService = inject(BlogPostService);
  categoryService = inject(CategoryService);
  router = inject(Router);

  private blogPostRef = this.blogPostService.getBlogPostById(this.id);
  blogPostResponse = this.blogPostRef.value;

  private categoriesRef = this.categoryService.getAllCategories();
  categoriesResponse = this.categoriesRef.value;

  editBlogPostForm = new FormGroup({
    title: new FormControl<string>('', {
      nonNullable: true,
      validators: [Validators.required, Validators.maxLength(100), Validators.minLength(10)]
    }),

    shortDescription: new FormControl<string>('', {
      nonNullable: true,
      validators: [Validators.required, Validators.maxLength(500), Validators.minLength(10)]
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

  effectRef = effect(() => {

    if (this.blogPostResponse()) {
      this.editBlogPostForm.patchValue({
        title: this.blogPostResponse()?.title,
        shortDescription: this.blogPostResponse()?.shortDescription,
        content: this.blogPostResponse()?.content,
        featuredImageUrl: this.blogPostResponse()?.featuredImageUrl,
        urlHandle: this.blogPostResponse()?.urlHandle,
        author: this.blogPostResponse()?.author,
        publishedDate: new Date(this.blogPostResponse()?.publishedDate!).toISOString().split('T')[0],
        categories: this.blogPostResponse()?.categories.map(c => c.id),
      });
    }
  });

  onSubmit() {

    const id = this.id();
    if (id && this.editBlogPostForm.valid) {
      const formRawValue = this.editBlogPostForm.getRawValue();

      const updateBlogPostRequest: UpdateBlogPostRequest =
      {
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
      this.blogPostService.updateBlogPost(id, updateBlogPostRequest)
        .subscribe({
          next: (response) => {
            console.log(response);
            this.router.navigate(['/admin/blogposts']);
          },
          error: () => {
            console.error('Something went wrong!');
          },
        });
    }
  }

  onDelete() {
    const id = this.id();
    if(id)
    {
      this.blogPostService.deleteBlogPost(id)
      .subscribe({
        next: (response) => {
          console.log(response);
          this.router.navigate(['/admin/blogposts']);
        },
        error: () => {
          console.error('Something went wrong!');
        },
      });
    }
  }
}

