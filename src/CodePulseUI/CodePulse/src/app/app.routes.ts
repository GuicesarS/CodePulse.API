import { Routes } from '@angular/router';
import { CategoryList } from './features/category/category-list/category-list';
import { AddCategory } from './features/category/add-category/add-category';
import { EditCategory } from './features/category/edit-category/edit-category';
import { BlogpostList } from './features/Blogpost/blogpost-list/blogpost-list';
import { AddBlogpost } from './features/Blogpost/add-blogpost/add-blogpost';
import { EditBlogPost } from './features/Blogpost/edit-blog-post/edit-blog-post';
import { BlogDetails } from './features/public/blog-details/blog-details';
import { Home } from './features/public/home/home';
import { Login } from './features/Auth/login/login';
import { adminGuard } from './features/Auth/guards/admin-guard';

export const routes: Routes = [
    {
        path: 'admin/categories',
        component: CategoryList,
        canActivate:[adminGuard]
    },
    {
        path: 'admin/categories/add',
        component: AddCategory,
        canActivate:[adminGuard]
    },
    {
        path: 'admin/categories/edit/:id',
        component: EditCategory,
        canActivate:[adminGuard]
    },
    {
        path: 'admin/blogposts',
        component: BlogpostList,
        canActivate:[adminGuard]
    },
    {
        path: 'admin/blogposts/add',
        component: AddBlogpost,
        canActivate:[adminGuard]
    },
    {
        path: 'admin/blogposts/edit/:id',
        component: EditBlogPost,
        canActivate:[adminGuard]
    },
    {
        path: 'blog/:url',
        component: BlogDetails
    },
    {
        path: '',
        component: Home
    },
    {
        path: 'login',
        component: Login
    }

];
