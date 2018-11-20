import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  terminyImg: any = '../../assets/events.jpg';
  novinkyImg: any = '../../assets/intro.jpg';
  constructor() { }

  ngOnInit() {
  }

}
