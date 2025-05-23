import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AdminService } from 'src/app/shared/services/admin.service';

@Component({
  selector: 'app-manage-cars',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './manage-cars.component.html'
})
export class ManageCarsComponent implements OnInit {
  cars: any[] = [];
  form: FormGroup;
  isEditing = false;

  constructor(private adminService: AdminService, private fb: FormBuilder) {
    this.form = this.fb.group({
      carId: [null],
      userId: ['', Validators.required],
      model: ['', Validators.required],
      licensePlate: ['', Validators.required],
      imageUrl: ['']
    });
  }

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.adminService.getCars().subscribe(res => this.cars = res);
  }

  edit(car: any) {
    this.isEditing = true;
    this.form.patchValue(car);
  }

  save() {
    if (this.form.invalid) return;
    this.adminService.createOrUpdateCar(this.form.value).subscribe(() => {
      this.form.reset();
      this.isEditing = false;
      this.load();
    });
  }

  delete(id: number) {
    this.adminService.deleteCar(id).subscribe(() => this.load());
  }
}
