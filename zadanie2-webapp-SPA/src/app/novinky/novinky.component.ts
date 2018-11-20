import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AlertifyService } from 'src/_services/alertify.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-novinky',
  templateUrl: './novinky.component.html',
  styleUrls: ['./novinky.component.css']
})
export class NovinkyComponent implements OnInit {
  novinky: any;
  pridavanieRezim: boolean;
  novinkaToAdd: any = {};
  novinkaToUpdate: any = {};
  constructor(private http: HttpClient, private alertify: AlertifyService) { }

  ngOnInit() {
    this.getNovinky();
    this.pridavanieRezim = false;
  }

  getNovinky() {
    this.http.get(environment.apiUrl + 'novinky').subscribe(response => {
      this.novinky = response;
    }, error => {
      this.alertify.error('Problem spojenia');
    });
  }

  pridavanieToggle() {
    this.pridavanieRezim = !this.pridavanieRezim;
  }

  addNovinka() {
    if (this.novinkaToAdd.text === undefined) {
      this.novinkaToAdd.text = 'Popis nebol zadaný.';
    }
    this.novinkaToAdd.datum = new Date(this.novinkaToAdd.datum).toUTCString();
    console.log(this.novinkaToAdd);
    this.http.post(environment.apiUrl + 'novinky', this.novinkaToAdd).subscribe(() => {
      this.alertify.success('Novinka bola úspešne pridaná');
    }, error => {
      this.alertify.error('Novinku sa nepodarilo pridať');
    });
  }

  updateNovinka(novinka) {
    console.log(novinka);
    this.novinkaToUpdate = novinka;
    this.http.put(environment.apiUrl + 'novinky/' + this.novinkaToUpdate.id, this.novinkaToUpdate).subscribe(() => {
      this.alertify.success('Novinka bola úspešne aktualizovaná');
    }, error => {
      this.alertify.error('Novinku sa nepodarilo aktualizovať');
    });
  }

}
