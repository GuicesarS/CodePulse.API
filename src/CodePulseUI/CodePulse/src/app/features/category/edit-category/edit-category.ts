import { Component, effect, inject, input } from '@angular/core';
import { CategoryService } from '../services/category-service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UpdateCategoryRequest } from '../models/category.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-edit-category',
  imports: [ReactiveFormsModule],
  templateUrl: './edit-category.html',
  styleUrl: './edit-category.css',
})
export class EditCategory {
  id = input<string>();

  constructor() {
    effect(() => {
      if (this.categoryService.editCategoryStatus() === 'success') {
        this.categoryService.editCategoryStatus.set('idle');
        this.router.navigate(['/admin/categories']);
      }

      if (this.categoryService.addCategoryStatus() === 'error') {
        console.error('Something went wrong!');
      }
    });
  }

  private router = inject(Router);
  private categoryService = inject(CategoryService);

  categoryResourceRef = this.categoryService.getCategoryById(this.id);

  categoryResponse = this.categoryResourceRef.value;

  editCategoryFormGroup = new FormGroup({

    categoryName: new FormControl<string>('', { nonNullable: true, validators: [Validators.required, Validators.maxLength(100)] }),
    categoryUrlHandle: new FormControl<string>('', { nonNullable: true, validators: [Validators.required, Validators.maxLength(200)] }),

  });

  get nameFormControl() {
    return this.editCategoryFormGroup.controls.categoryName;
  }

  get urlHandleFormControl() {
    return this.editCategoryFormGroup.controls.categoryUrlHandle;
  }

  effectRef = effect(() => {
    this.editCategoryFormGroup.controls.categoryName.patchValue(this.categoryResponse()?.name ?? '');
    this.editCategoryFormGroup.controls.categoryUrlHandle.patchValue(this.categoryResponse()?.urlHandle ?? '');
  })

  onSubmit() {
    const id = this.id();

    if (!this.editCategoryFormGroup.valid || !id)
      return;

    const formRawValue = this.editCategoryFormGroup.getRawValue();

    const updateCategoryDto: UpdateCategoryRequest = {
      name: formRawValue.categoryName,
      urlHandle: formRawValue.categoryUrlHandle,
    };

    this.categoryService.updateCategory(id, updateCategoryDto);

  }

  deleteCategory()
  {
    const id = this.id();
      if(!id)
      return;

      this.categoryService.deleteCategory(id)
      .subscribe({
        next: () => {
          this.router.navigate(['/admin/categories']);
        },
        error: () => {
          console.error('Something went wrong!');
        }

      });
  }

}
