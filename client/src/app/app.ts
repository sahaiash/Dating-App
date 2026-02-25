import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-root',
  imports: [],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App implements OnInit {
  private http=inject(HttpClient);
  protected  title = 'Dating app';
  protected members = signal<any>([]);
  
  async ngOnInit(){
  //  this.http.get('https://localhost:5001/api/members').subscribe({
  //   next: response=> {
  //     console.log("API response :", response);
  //     this.members.set(response);
  //   },
  //   error: err=>console.log(err),
  //   complete:()=>console.log('Completed Http Request')
  //  });
      this.members.set(await this.getMembers());
  }


  async getMembers(){
    try {
      return await lastValueFrom(this.http.get('https://localhost:5001/api/members'));
    } catch (error) {
      console.log(error);
      throw error;
    }
  }

}
