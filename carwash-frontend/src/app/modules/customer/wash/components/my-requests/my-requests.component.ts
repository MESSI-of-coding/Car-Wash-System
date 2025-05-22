import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { WashService } from 'src/app/core/services/wash.service';
import { WashRequest } from 'src/app/core/models/wash-request.model';

@Component({
  selector: 'app-my-requests',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './my-requests.component.html',
  styleUrls: ['./my-requests.component.scss']
})
export class MyRequestsComponent implements OnInit {
  active: 'pending' | 'inProcess' | 'completed' = 'pending';
  pending: WashRequest[] = [];
  inProcess: WashRequest[] = [];
  completed: WashRequest[] = [];

  constructor(private washService: WashService) {}

  ngOnInit(): void {
    this.washService.getRequestsByUser().subscribe(requests => {
      this.pending   = requests.filter(r => r.status === 'Pending');
      this.inProcess = requests.filter(r => r.status === 'InProgress');
      this.completed = requests.filter(r => r.status === 'Completed');
    });
  }
}
