import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditBlogPost } from './edit-blog-post';

describe('EditBlogPost', () => {
  let component: EditBlogPost;
  let fixture: ComponentFixture<EditBlogPost>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditBlogPost]
    })
    .compileComponents();

    fixture = TestBed.createComponent(EditBlogPost);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
