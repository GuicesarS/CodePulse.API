import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-add-category',
  imports: [ReactiveFormsModule],
  templateUrl: './add-category.html',
  styleUrl: './add-category.css',
})
export class AddCategory {
    // 1 Import ReactiveFormsModule
    // 2 FormGroups -> FormControl
    
    // Create a FormGroup to manage the entire form state
    addCategoryFormGroup = new FormGroup({
      // FormControl for category name field
      // Empty string as default value, nonNullable ensures it's never null
      categoryName: new FormControl<string>('', { nonNullable: true }),
      
      // FormControl for URL handle (slug) field  
      // Used for SEO-friendly URLs (e.g., "my-category" instead of "My Category")
      categoryUrlHandle: new FormControl<string>('', { nonNullable: true }),
    });

    // Handler method called when form is submitted
    onSubmit() {
      // Get all form values and log to console for debugging
      // getRawValue() returns the current values of all form controls
      console.log(this.addCategoryFormGroup.getRawValue());
    }
}
