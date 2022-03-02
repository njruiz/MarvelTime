import { Component, OnInit } from '@angular/core';
import { UserParams } from '../_models/userParams';
import { AccountService } from '../_services/account.service';
import { MembersService } from '../_services/members.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  userParams: UserParams;
  registerMode = false;

  constructor(
    public accountService: AccountService,
    private memberService: MembersService
  ) {
    this.userParams = this.memberService.getUserParams();
  }

  ngOnInit(): void {}

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }
}
