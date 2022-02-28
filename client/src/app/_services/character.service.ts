import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Character } from '../_models/character';

@Injectable({
  providedIn: 'root',
})
export class CharacterService {
  baseUrl = environment.apiUrl;
  character: Character;

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
    return this.http.get<Partial<Character[]>>(this.baseUrl + 'characters');
  }

  getCharacter(characterId: string) {
    return this.http.get<Character>(this.baseUrl + 'characters/' + characterId);
  }
}
