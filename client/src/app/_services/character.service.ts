import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { of } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Character } from '../_models/character';

@Injectable({
  providedIn: 'root',
})
export class CharacterService {
  baseUrl = environment.apiUrl;
  characters: Character[] = [];

  constructor(private http: HttpClient) {}

  addCharacter(model: any) {
    return this.http
      .post(this.baseUrl + 'characters/create-character', model)
      .pipe(
        map((character: Character) => {
          if (character) {
            console.log(character);
          }
        })
      );
  }

  getCharacters() {
    if (this.characters.length > 0) return of(this.characters);

    return this.http.get<Character[]>(this.baseUrl + 'characters').pipe(
      map((characters) => {
        this.characters = characters;
        return characters;
      })
    );
  }

  getCharacter(characterId: string) {
    this.getCharacters();
    const character = this.characters.find(
      (x) => x.characterId === characterId
    );

    if (character !== undefined) return of(character);

    return this.http.get<Character>(this.baseUrl + 'characters/' + characterId);
  }

  updateCharacter(character: Character) {
    return this.http
      .put(this.baseUrl + 'characters/' + character.characterId, character)
      .pipe(
        map(() => {
          const index = this.characters.indexOf(character);
          this.characters[index] = character;
        })
      );
  }

  setMainPhoto(photoId: number, characterId: string) {
    return this.http.put(
      this.baseUrl + 'characters/' + characterId + '/set-main-photo/' + photoId,
      {}
    );
  }

  deletePhoto(photoId: number, characterId: string) {
    return this.http.delete(
      this.baseUrl + 'characters/' + characterId + '/delete-photo/' + photoId
    );
  }
}
