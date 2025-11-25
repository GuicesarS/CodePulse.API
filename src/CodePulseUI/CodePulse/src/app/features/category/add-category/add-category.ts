import { Component, effect, inject } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AddCategoryRequest } from '../models/category.model';
import { CategoryService } from '../services/category-service';

@Component({
  selector: 'app-add-category',
  imports: [ReactiveFormsModule],
  templateUrl: './add-category.html',
  styleUrl: './add-category.css',
})
export class AddCategory {

  constructor() {
    effect(() => {
      if (this.categoryService.addCategoryStatus() === 'success') {
        console.log('Success');
        
      }

      if (this.categoryService.addCategoryStatus() === 'error') {
        console.log('Add Category Request Failed');
      }
    });
  }

  private categoryService = inject(CategoryService);

  addCategoryFormGroup = new FormGroup({

    categoryName: new FormControl<string>('', { nonNullable: true, validators: [Validators.required, Validators.maxLength(100)] }),
    categoryUrlHandle: new FormControl<string>('', { nonNullable: true, validators: [Validators.required, Validators.maxLength(200)] }),

  });

  get nameFormControl() {
    return this.addCategoryFormGroup.controls.categoryName;
  }

  get urlHandleFormControl() {
    return this.addCategoryFormGroup.controls.categoryUrlHandle;
  }

  onSubmit() {
    const addCategoryFormValue = (this.addCategoryFormGroup.getRawValue());

    const AddCategoryRequestDto: AddCategoryRequest = {
      name: addCategoryFormValue.categoryName,
      urlHandle: addCategoryFormValue.categoryUrlHandle
    };

    this.categoryService.addCategory(AddCategoryRequestDto);
  }
}
