import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Stripe } from '@stripe/stripe-js';
import { environment } from 'src/environments/environment';
import { PaymentService } from 'src/app/core/services/payment.service';
import { StripeService } from 'src/app/shared/services/stripe.service';

@Component({
  selector: 'app-payment',
  standalone: true,
  imports: [],
  templateUrl: './payment.component.html',
  styleUrl: './payment.component.scss'
})
export class PaymentComponent implements OnInit {
  clientSecret: string = '';
  cardElement!: any;
  stripe!: Stripe | null;
  requestId: string = '';
  paymentSuccess = false;
  errorMessage = '';
  cardError: string = '';        // <-- Added
  processing: boolean = false;   // <-- Added

  constructor(
    private route: ActivatedRoute,
    private paymentService: PaymentService,
    private stripeService: StripeService,
    private router: Router
  ) {}

  async ngOnInit() {
    this.requestId = this.route.snapshot.paramMap.get('requestId')!;
    this.paymentService.getClientSecret(this.requestId).subscribe({
      next: async (result) => {
        this.clientSecret = result.clientSecret;
        this.stripe = await this.stripeService.getStripeInstance();
        if (!this.stripe) {
          this.errorMessage = 'Stripe failed to load.';
          return;
        }
        const elements = this.stripe.elements();
        this.cardElement = elements.create('card');
        this.cardElement.mount('#card-element');
        this.cardElement.on('change', (event: any) => {
          this.cardError = event.error ? event.error.message : '';
        });
      },
      error: (err) => {
        this.errorMessage = err?.message || 'Failed to initialize payment.';
      }
    });
  }

  async handlePayment() {
    if (!this.stripe || !this.clientSecret) return;
    this.processing = true;
    const result = await this.stripeService.confirmPayment(this.clientSecret, this.cardElement);
    const { paymentIntent, error } = result;
    if (error) {
      this.cardError = error.message ?? 'Payment failed.';
      this.processing = false;
    } else if (paymentIntent?.status === 'succeeded') {
      this.paymentSuccess = true;
      this.processing = false;
      setTimeout(() => this.router.navigate(['/customer/wash/requests']), 2000);
    }
  }

  goBack() {
    this.router.navigate(['/customer/wash/requests']);
  }

  closeModal() {
    this.paymentSuccess = false;
    this.router.navigate(['/customer/wash/requests']);
  }
}