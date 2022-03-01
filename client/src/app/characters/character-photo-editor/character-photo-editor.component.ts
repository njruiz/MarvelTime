import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FileUploader } from 'ng2-file-upload';
import { take } from 'rxjs/operators';
import { Character } from 'src/app/_models/character';
import { Photo } from 'src/app/_models/photo';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { CharacterService } from 'src/app/_services/character.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-character-photo-editor',
  templateUrl: './character-photo-editor.component.html',
  styleUrls: ['./character-photo-editor.component.css'],
})
export class CharacterPhotoEditorComponent implements OnInit {
  character: Character;
  user: User;
  hasBaseDropzoneOver = false;
  uploader: FileUploader;
  baseUrl = environment.apiUrl;

  constructor(
    private accountService: AccountService,
    private characterService: CharacterService,
    private route: ActivatedRoute
  ) {
    this.accountService.currentUser$
      .pipe(take(1))
      .subscribe((user) => (this.user = user));
  }

  ngOnInit(): void {
    this.loadCharacter();
    this.initializeUploader();
  }

  loadCharacter() {
    this.characterService
      .getCharacter(this.route.snapshot.paramMap.get('characterId'))
      .subscribe((character) => {
        this.character = character;
      });
  }

  setMainPhoto(photo: Photo) {
    this.characterService
      .setMainPhoto(photo.id, this.character.characterId)
      .subscribe(() => {
        this.character.photoUrl = photo.url;
        this.character.photos.forEach((p) => {
          if (p.isMain) p.isMain = false;
          if (p.id === photo.id) p.isMain = true;
        });
      });
  }

  deletePhoto(photoId: number) {
    this.characterService
      .deletePhoto(photoId, this.character.characterId)
      .subscribe(() => {
        this.character.photos = this.character.photos.filter(
          (x) => x.id !== photoId
        );
      });
  }

  fileOverBase(e: any) {
    this.hasBaseDropzoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url:
        this.baseUrl +
        'characters/' +
        this.character.characterId +
        '/add-photo',
      authToken: 'Bearer ' + this.user.token,
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024,
    });

    this.uploader.onAfterAddingFile = (file) => {
      file.withCredentials = false;
    };

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const photo: Photo = JSON.parse(response);
        this.character.photos.push(photo);
        if (photo.isMain) {
          this.character.photoUrl = photo.url;
        }
      }
    };
  }
}
