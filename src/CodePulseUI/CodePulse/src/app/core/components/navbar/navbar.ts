import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink], // Import RouterLink for navigation
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar {

}
