import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/shared/services/admin.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-manage-users',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './manage-users.component.html',
  styleUrl: './manage-users.component.scss'
})
export class ManageUsersComponent implements OnInit {
  users: any[] = [];

  constructor(private adminService: AdminService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers() {
    this.adminService.getUsers().subscribe(data => {
      this.users = data;
    });
  }

  toggleStatus(user: any) {
    const newStatus = !user.isActive;
    this.adminService.toggleUserStatus(user.userId, newStatus).subscribe(() => {
      user.isActive = newStatus;
    });
  }

  editUser(userId: string) {
    // TODO: open modal or navigate to UserEditComponent
    console.log('Edit user:', userId);
  }
}
