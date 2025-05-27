import { Component, Input, Output, EventEmitter } from '@angular/core';
import { AddOnDto } from './manage-addons.component';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-addon-modal',
  standalone: true,
  imports: [ReactiveFormsModule], 
  templateUrl: './addon-modal.component.html',
})
export class AddonModalComponent {
  @Input() addon: AddOnDto | null = null;
  @Output() close = new EventEmitter<AddOnDto | boolean>(); // Emit data or boolean
  form: FormGroup;

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      addOnId: [null],
      name: ['', Validators.required],
      description: [''],
      price: ['', [Validators.required, Validators.min(0)]]
    });
  }

  ngOnInit() {
    if (this.addon) {
      this.form.patchValue(this.addon);
    }
  }

  onSubmit() {
    if (this.form.valid) {
      this.close.emit(this.form.value); // Emit form data
    }
  }

  onClose() {
    this.close.emit(false); // Emit false to cancel
  }
}