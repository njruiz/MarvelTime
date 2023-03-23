import { Component, OnInit } from '@angular/core';
import { Character } from 'src/app/_models/character';
import { Pagination } from 'src/app/_models/pagination';
import { CharacterService } from 'src/app/_services/character.service';

@Component({
  selector: 'app-favorites',
  templateUrl: './favorites.component.html',
  styleUrls: ['./favorites.component.css'],
})
export class FavoritesComponent implements OnInit {
  characters: Partial<Character[]>;
  predicate = 'liked';
  pageNumber = 1;
  pageSize = 5;
  pagination: Pagination;

  constructor(private characterService: CharacterService) {}

  ngOnInit(): void {
    this.loadLikes();
  }

  loadLikes() {
    this.characterService
      .getLikes(this.predicate, this.pageNumber, this.pageSize)
      .subscribe((response) => {
        this.characters = response.result;
        this.pagination = response.pagination;
      });
  }

  pageChanged(event: any) {
    this.pageNumber = event.page;
    this.loadLikes();
  }
}
