import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Character } from 'src/app/_models/character';
import { CharacterService } from 'src/app/_services/character.service';

@Component({
  selector: 'app-character-card',
  templateUrl: './character-card.component.html',
  styleUrls: ['./character-card.component.css'],
})
export class CharacterCardComponent implements OnInit {
  @Input() character: Character;

  constructor(
    private characterService: CharacterService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {}

  addLike(character: Character) {
    this.characterService.addLike(character.characterId).subscribe(() => {
      this.toastr.success('You have liked ' + character.characterName);
    });
  }
}
