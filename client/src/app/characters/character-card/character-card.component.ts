import { Component, Input, OnInit } from '@angular/core';
import { Character } from 'src/app/_models/character';

@Component({
  selector: 'app-character-card',
  templateUrl: './character-card.component.html',
  styleUrls: ['./character-card.component.css'],
})
export class CharacterCardComponent implements OnInit {
  @Input() character: Character;

  constructor() {}

  ngOnInit(): void {}
}
