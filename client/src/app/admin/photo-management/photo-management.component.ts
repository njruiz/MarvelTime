import { Component, OnInit } from '@angular/core';
import { Photo } from 'src/app/_models/photo';
import { AdminService } from 'src/app/_services/admin.service';
import { ConfirmService } from 'src/app/_services/confirm.service';

@Component({
  selector: 'app-photo-management',
  templateUrl: './photo-management.component.html',
  styleUrls: ['./photo-management.component.css'],
})
export class PhotoManagementComponent implements OnInit {
  photos: Photo[];

  constructor(
    private adminService: AdminService,
    private confirmService: ConfirmService
  ) {}

  ngOnInit(): void {
    this.getPhotosForApproval();
  }

  getPhotosForApproval() {
    this.adminService.getPhotosForApproval().subscribe((photos) => {
      this.photos = photos;
    });
  }

  approvePhoto(photoId: number) {
    this.confirmService
      .confirm('Confirm photo approval')
      .subscribe((result) => {
        this.adminService.approvePhoto(photoId).subscribe(() => {
          this.photos.splice(
            this.photos.findIndex((p) => p.id === photoId),
            1
          );
        });
      });
  }

  rejectPhoto(photoId: number) {
    this.confirmService
      .confirm('Confirm photo rejection')
      .subscribe((result) => {
        this.adminService.rejectPhoto(photoId).subscribe(() => {
          this.photos.splice(
            this.photos.findIndex((p) => p.id === photoId),
            1
          );
        });
      });
  }
}
