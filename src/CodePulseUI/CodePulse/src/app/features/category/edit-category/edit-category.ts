import { Component, effect, inject, input } from '@angular/core';
import { CategoryService } from '../services/category-service';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-edit-category',
  imports: [ReactiveFormsModule],
  templateUrl: './edit-category.html',
  styleUrl: './edit-category.css',
})
export class EditCategory {
  id = input<string>();

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

  onSubmit()
  {
    console.log(this.editCategoryFormGroup.getRawValue());
    console.log(this.id());
  }

}
