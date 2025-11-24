import { Routes } from '@angular/router';
import { CategoryList } from './features/category/category-list/category-list';

export const routes: Routes = [
    {
        path: 'admin/categories',    // URL path for categories page
        component: CategoryList      // Component to display when visiting the path
    }
];
