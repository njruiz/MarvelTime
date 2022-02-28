import { Component, OnInit } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { CreateCharacterModalComponent } from 'src/app/modals/create-character-modal/create-character-modal.component';
import { Character } from 'src/app/_models/character';
import { CharacterService } from 'src/app/_services/character.service';

@Component({
  selector: 'app-character-management',
  templateUrl: './character-management.component.html',
  styleUrls: ['./character-management.component.css'],
})
export class CharacterManagementComponent implements OnInit {
  characters: Partial<Character[]>;
  constructor(
    private modalService: BsModalService,
    private characterService: CharacterService
  ) {}

  ngOnInit(): void {
    this.getCharacters();
  }

  openCreateCharacterModal() {
    this.modalService.show(CreateCharacterModalComponent);
  }

  getCharacters() {
    this.characterService.getCharacters().subscribe((characters) => {
      this.characters = characters;
    });
  }
}
