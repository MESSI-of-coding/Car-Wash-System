import { Injectable } from '@angular/core';
import { loadStripe, Stripe, StripeCardElement } from '@stripe/stripe-js';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StripeService {
  private stripe: Stripe | null = null;

  constructor() {
    // Stripe initialization is async, so don't block constructor
    this.initializeStripe();
  }

  private async initializeStripe() {
    if (!this.stripe) {
      this.stripe = await loadStripe(environment.stripePublishableKey);
      if (!this.stripe) {
        console.error('Stripe failed to load.');
      }
    }
  }

  async confirmPayment(clientSecret: string, card: StripeCardElement): Promise<any> {
    if (!this.stripe) {
      this.stripe = await loadStripe(environment.stripePublishableKey);
    }
    if (!this.stripe) {
      throw new Error('Stripe not initialized');
    }
    return this.stripe.confirmCardPayment(clientSecret, {
      payment_method: { card }
    });
  }

  async getStripeInstance(): Promise<Stripe | null> {
    if (!this.stripe) {
      this.stripe = await loadStripe(environment.stripePublishableKey);
    }
    return this.stripe;
  }
}
