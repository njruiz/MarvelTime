import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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
  favoriteCharacter: any;
  likedCharacters: Partial<Character[]>;
  predicate = 'liked';
  pageNumber = 1;
  pageSize = 5;
  initState = true;

  constructor(
    private characterService: CharacterService,
    private toastr: ToastrService, 
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadLikes();
  }

  addLike(character: Character) {
    this.initState = false;
    
    this.characterService.addLike(character.characterId).subscribe((response) => {
      let obj = JSON.parse(JSON.stringify(response))
      this.favoriteCharacter = obj.like_status;
      const currentRoute = this.router.url;
      if (this.favoriteCharacter == true)
      {
        this.toastr.success('You have liked ' + character.characterName);
      }
      else
      {
        this.toastr.error('You have unliked ' + character.characterName);
        if (location.pathname == "/favorites")
        {
          this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
            this.router.navigate([currentRoute]);
          });
        }
        else
          this.router.navigateByUrl('/');
      }
    });
  }

  loadLikes() {
    this.characterService
      .getLikes(this.predicate, this.pageNumber, this.pageSize)
      .subscribe((response) => {
        this.likedCharacters = response.result;
      });
  }

  isCharacterLiked(character: Character) {
    let flag = false;
    //console.log("Characters: " + JSON.stringify(this.likedCharacters))
    if (this.likedCharacters?.indexOf(character) !== -1)
    {
      flag = true;
    }
    
    return flag;
  }
}
