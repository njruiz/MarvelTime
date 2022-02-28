import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Character } from 'src/app/_models/character';
import { CharacterService } from 'src/app/_services/character.service';

@Component({
  selector: 'app-character-detail',
  templateUrl: './character-detail.component.html',
  styleUrls: ['./character-detail.component.css'],
})
export class CharacterDetailComponent implements OnInit {
  character: Character;

  constructor(
    private characterService: CharacterService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.loadCharacter();
  }

  loadCharacter() {
    this.characterService
      .getCharacter(this.route.snapshot.paramMap.get('characterId'))
      .subscribe((character) => {
        this.character = character;
      });
  }
}
