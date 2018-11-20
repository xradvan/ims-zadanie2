import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { AlertifyService } from 'src/_services/alertify.service';

@Component({
  selector: 'app-terminy',
  templateUrl: './terminy.component.html',
  styleUrls: ['./terminy.component.css']
})
export class TerminyComponent implements OnInit {
  terminy: any;
  pridavanieRezim: boolean;
  terminToAdd: any = {};
  terminToUpdate: any = {};
  constructor(private http: HttpClient, private alertify: AlertifyService) { }

  ngOnInit() {
    this.getTerminy();
    this.pridavanieRezim = false;
  }

  getTerminy() {
    this.http.get(environment.apiUrl + 'terminy').subscribe(response => {
      this.terminy = response;
    }, error => {
      this.alertify.error('Problem spojenia');
    });
  }

  pridavanieToggle() {
    this.pridavanieRezim = !this.pridavanieRezim;
  }

  addTermin() {
    if (this.terminToAdd.text === undefined) {
      this.terminToAdd.text = 'Popis nebol zadaný.';
    }
    this.terminToAdd.datum = new Date(this.terminToAdd.datum).toUTCString();
    this.http.post(environment.apiUrl + 'terminy', this.terminToAdd).subscribe(() => {
      this.alertify.success('Termín bol úspešne pridaný');
    }, error => {
      this.alertify.error('Termin sa nepodarilo pridat');
    });
  }

  updateTermin(termin) {
    console.log(termin);
    this.terminToUpdate = termin;
    // this.terminToUpdate.datum = new Date(this.terminToUpdate.datum).toUTCString();
    this.http.put(environment.apiUrl + 'terminy/' + this.terminToUpdate.id, this.terminToUpdate).subscribe(() => {
      this.alertify.success('Termín bol úspešne aktualizovaný');
    }, error => {
      this.alertify.error('Termin sa nepodarilo aktualizovat');
    });
  }

}
