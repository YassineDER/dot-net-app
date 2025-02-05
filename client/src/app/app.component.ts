import {Component, inject, OnInit} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {HttpClient} from "@angular/common/http";

@Component({
    selector: 'app-root',
    standalone: true,
    imports: [RouterOutlet],
    templateUrl: './app.component.html',
    styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
    http = inject(HttpClient);
    api = 'https://localhost:5001/api/users';
    users :any;

    ngOnInit() {
        this.http.get(this.api).subscribe({
            next: data => this.users = data,
            error: error => console.error(error),
            complete: () => console.log('completed fetching users')
        });
    }

}
