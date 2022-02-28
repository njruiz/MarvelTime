import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Character } from '../_models/character';
import { Member } from '../_models/member';
import { Pagination } from '../_models/pagination';
import { User } from '../_models/user';
import { UserParams } from '../_models/userParams';
import { AccountService } from '../_services/account.service';
import { CharacterService } from '../_services/character.service';
import { MembersService } from '../_services/members.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  characters: Character[];
  characters$: Observable<Character[]>;
  members: Member[];
  pagination: Pagination;
  userParams: UserParams;
  user: User;
  registerMode = false;

  constructor(
    public accountService: AccountService,
    private memberService: MembersService,
    private characterService: CharacterService
  ) {
    this.userParams = this.memberService.getUserParams();
  }

  ngOnInit(): void {
    this.characters$ = this.characterService.getCharacters();
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }
}
