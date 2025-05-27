import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WasherService } from '../../services/washer.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-washer-notification',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.scss']
})
export class WasherNotificationComponent implements OnInit {
  notifications: any[] = [];

  constructor(private washerService: WasherService) {}

  ngOnInit(): void {
    this.washerService.getNotifications().subscribe(data => {
      this.notifications = data;
    });
  }
}
