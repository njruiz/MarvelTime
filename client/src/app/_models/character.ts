import { Photo } from './photo';

export interface Character {
  realName: string;
  characterName: string;
  characterId: string;
  photoUrl: string;
  gender: string;
  role: string;
  id: number;
  dateOfBirth: Date;
  sourceOfPower?: any;
  mcuStory?: any;
  placeOfOrigin?: any;
  playedBy?: any;
  photos: Photo[];
}
