import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { CharacterService } from 'src/app/_services/character.service';

@Component({
  selector: 'app-create-character-modal',
  templateUrl: './create-character-modal.component.html',
  styleUrls: ['./create-character-modal.component.css'],
})
export class CreateCharacterModalComponent implements OnInit {
  characterForm: UntypedFormGroup;
  // Roles
  Roles: any = ['Superhero', 'Villain', 'Civilian', 'Neutral'];
  validationErrors: string[] = [];

  constructor(
    private characterService: CharacterService,
    private fb: UntypedFormBuilder,
    public bsModalRef: BsModalRef,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.characterForm = this.fb.group({
      gender: ['male'],
      characterName: ['', Validators.required],
      realName: ['', Validators.required],
      playedBy: ['', Validators.required],
      dateOfBirth: ['', Validators.required],
      placeOfOrigin: ['', Validators.required],
      role: ['', Validators.required],
    });
  }

  get roleName() {
    return this.characterForm.get('role');
  }

  changeRole(e) {
    this.roleName.setValue(e.target.value, {
      onlySelf: true,
    });
  }

  addCharacter() {
    this.characterService.addCharacter(this.characterForm.value).subscribe(
      (response) => {
        this.router.navigateByUrl('admin');
        this.bsModalRef.hide();
      },
      (error) => {
        this.validationErrors = error;
      }
    );
  }
}
