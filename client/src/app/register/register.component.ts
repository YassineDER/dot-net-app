import {Component, EventEmitter, input, output} from '@angular/core';
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
    usersFromHomeComponent = input.required<any[]>();
    cancelRegister = output<boolean>();
    model: any = {};

    register()   {
        console.log(this.model);
    }

    cancel() {
        // console.log('cancelled');
        // console.log(this.usersFromHomeComponent().map(user => user.userName));
        this.cancelRegister.emit(false);
    }
}
