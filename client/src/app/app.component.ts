import {Component, inject, OnInit} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {NavComponent} from "./nav/nav.component";
import {AccountService} from "./_services/account.service";
import {User} from "./_models/user";

@Component({
    selector: 'app-root',
    standalone: true,
    imports: [RouterOutlet, NavComponent],
    templateUrl: './app.component.html',
    styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
    private accountService = inject(AccountService);
    users :any;

    ngOnInit() {
        this.getUsers();
        this.setCurrentUser();
    }

    setCurrentUser() {
        const userString = localStorage.getItem('user');
        if (!userString) return;
        const user = JSON.parse(userString) as User;
        this.accountService.currentUser.set(user);
    }


    getUsers() {
        this.accountService.getUsers().subscribe({
            next: users => this.users = users,
            error: error => console.log(error)
        });
    }

}
