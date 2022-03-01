import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs/operators';
import { Character } from 'src/app/_models/character';
import { CharacterService } from 'src/app/_services/character.service';

@Component({
  selector: 'app-character-edit',
  templateUrl: './character-edit.component.html',
  styleUrls: ['./character-edit.component.css'],
})
export class CharacterEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm;
  character: Character;

  @HostListener('window:beforeunload', ['$event']) unloadNotification(
    $event: any
  ) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(
    private characterService: CharacterService,
    private toastr: ToastrService,
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

  updateCharacter() {
    this.characterService.updateCharacter(this.character).subscribe(() => {
      this.toastr.success('Profile updated successfully');
      this.editForm.reset(this.character);
    });
  }
}
