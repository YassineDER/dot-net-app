import {Component, inject, OnInit} from '@angular/core';
import {RegisterComponent} from "../register/register.component";
import {AccountService} from "../_services/account.service";

@Component({
  selector: 'app-home',
  standalone: true,
    imports: [
        RegisterComponent
    ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
    accountService = inject(AccountService);
    users: any;
    registering = false;

    ngOnInit(): void {
        this.getUsers();
    }

    registerToggle() {
        this.registering = !this.registering;
    }

    getUsers() {
        this.accountService.getUsers().subscribe({
            next: users => this.users = users,
            error: error => console.log(error)
        });
    }

    cancelRegisterMode(event: boolean) {
        this.registering = event;
    }
}
