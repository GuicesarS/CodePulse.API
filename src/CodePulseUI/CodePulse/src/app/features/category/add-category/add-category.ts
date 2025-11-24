import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-category',
  imports: [ReactiveFormsModule],
  templateUrl: './add-category.html',
  styleUrl: './add-category.css',
})
export class AddCategory {

  addCategoryFormGroup = new FormGroup({

    categoryName: new FormControl<string>('', { nonNullable: true, validators: [Validators.required, Validators.maxLength(100)] }),

    categoryUrlHandle: new FormControl<string>('', { nonNullable: true, validators: [Validators.required, Validators.maxLength(200)] }),
  });

  get nameFormControl()
  {
    return this.addCategoryFormGroup.controls.categoryName;
  }

  get urlHandleFormControl()
  {
    return this.addCategoryFormGroup.controls.categoryUrlHandle;
  }

  onSubmit() {
    console.log(this.addCategoryFormGroup.getRawValue());
  }
}
